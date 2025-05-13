using Azure.Storage.Blobs;
using EventEase.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventEase.Models;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

namespace EventEase.Controllers
{
    public class BookingViewController : Controller
    {
        private readonly EventEaseDbContext dbContext;


        //        //Integrate azure blob storage
        private readonly string storageAccountName;
        private readonly string storageAccountKey;
        private readonly string containerName;
        public BookingViewController(EventEaseDbContext dbContext, IConfiguration config)
        {
            this.dbContext = dbContext;
            storageAccountName = config["AzureBlob:storageAccountname"] ?? throw new ArgumentNullException("AzureBlob:storageAccountname"); ;
            storageAccountKey = config["AzureBlob:storageAccountKey"] ?? throw new ArgumentNullException("AzureBlob:storageAccountKey");
            containerName = config["AzureBlob:containerName"] ?? "imageurl";
        }

        //        //Set up blob container client
        private BlobContainerClient GetContainerClient()
        {
            var serviceUri = new Uri($"https://{storageAccountName}.blob.core.windows.net");
            var serviceClient = new BlobServiceClient(serviceUri, new StorageSharedKeyCredential(storageAccountName, storageAccountKey));
            return serviceClient.GetBlobContainerClient(containerName);
        }

        //Set up image upload method
        private async Task<string> UploadImageToBlobAsync(IFormFile file)
        {
            //check if there is a valid image
            if (file == null || file.Length == 0) return null;

            //Create the container if doesn't exist
            var containerClient = GetContainerClient();
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.None);

            //Assign the image a unique ID and name
            var blobName = $"{Guid.NewGuid()}{Path.GetExtension(file.Name)}";
            var blobClient = containerClient.GetBlobClient(blobName);

            //Send image to container using blobClient
            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });

            return blobName;
        }

        //Set up our Shared access signature token method
        private string GenerateSasUrl(string blobName)
        {
            var blobUri = new Uri($"https://{storageAccountName}.blob.core.windows.net/{containerName}/{blobName}");
            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = containerName,
                BlobName = blobName,
                Resource = "b",
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(15)  //short-lived SAS
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            var sasToken = sasBuilder.ToSasQueryParameters(new StorageSharedKeyCredential(storageAccountName,storageAccountKey)).ToString();
            return $"{blobUri}?{sasToken}";
        }

        //Read all of the BookingView in a list form
        //GET: BookingView List
        [HttpGet]
        public async Task<IActionResult> Index(string searchQuery)
        {
            var bookingViews = dbContext.BookingViews.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                bookingViews = bookingViews.Where(l =>
                l.BookingViewid.ToString().Contains(searchQuery) ||
                l.EventName.Contains(searchQuery) ||
                l.VenueName.Contains(searchQuery));
            }

            //Fetch data from database
            var BookingViewsList = await bookingViews.ToListAsync();
            var BookingViewWithUrls = BookingViewsList.Select(l => new BookingViewViewModel
            {
                BookingViewid = l.BookingViewid,
                EventName = l.EventName,
                EventDate = l.EventDate,
                ImageURL = GenerateSasUrl(l.ImageURL),
                Location = l.Location,
                Capacity = l.Capacity,
                VenueName = l.VenueName,
                BookingDate = l.BookingDate,
                Description = l.Description,

            }).ToList();
            return View(BookingViewWithUrls);
        }

        //Get the details of a single BookingView
        //GET: Specific BookingView Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var details = await dbContext.BookingViews.FirstOrDefaultAsync(m => m.BookingViewid == id);

            if (details == null)
            {
                return NotFound();
            }
            return View(details);
        }

        //Create a new BookingView
        //GET: Create BookingView form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //POST: Create a new BookingView
        [HttpPost]
        public async Task<IActionResult> Create(BookingViewViewModel viewModel, IFormFile ImageURL)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string blobName = await UploadImageToBlobAsync(ImageURL);
            var BookingView = new BookingView
            {

                EventName = viewModel.EventName,
                EventDate = viewModel.EventDate,
                ImageURL = blobName,
                Location = viewModel.Location,
                Capacity = viewModel.Capacity,
                VenueName = viewModel.VenueName,
                BookingDate = viewModel.BookingDate,
                Description = viewModel.Description,
            };
            await dbContext.BookingViews.AddAsync(BookingView);
            await dbContext.SaveChangesAsync();

            //Set TempData Message
            TempData["SuccessMessage"] = $"BookingView {BookingView.EventName} was added successfully";
            return RedirectToAction("Index");
        }

        //Update an existing BookingView
        //GET: Update BookingView form
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var BookingView = await dbContext.BookingViews.FindAsync(id);
            if (BookingView == null)
            {
                return NotFound();
            }

            return View(BookingView);
        }

        //POST: Update an existing BookingView details
        [HttpGet]
        public async Task<IActionResult> Edit(BookingView viewModel)
        {
            var BookingView = await dbContext.BookingViews.FindAsync(viewModel.BookingViewid);
            if (BookingView is not null)
            {
                BookingView.BookingViewid = viewModel.BookingViewid;
                BookingView.EventName = viewModel.EventName;
                BookingView.EventDate = viewModel.EventDate;
                BookingView.ImageURL = viewModel.ImageURL;
                BookingView.Location = viewModel.Location;
                BookingView.Capacity = viewModel.Capacity;
                BookingView.VenueName = viewModel.VenueName;
                BookingView.BookingDate = viewModel.BookingDate;
                BookingView.Description = viewModel.Description;

                await dbContext.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }

        //Delete an existing BookingView
        //GET: Delete BookingView form
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var BookingView = await dbContext.BookingViews.FirstOrDefaultAsync(m => m.BookingViewid == id);
            if (BookingView == null)
            {
                return NotFound();
            }
            return View(BookingView);
        }

        //POST: Delete an existing BookingView details
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var BookingView = await dbContext.BookingViews.FindAsync(id);
            dbContext.BookingViews.Remove(BookingView);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

