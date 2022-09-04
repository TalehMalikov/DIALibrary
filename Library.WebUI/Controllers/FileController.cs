using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;
using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
using Library.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using File = Library.Entities.Concrete.File;

namespace Library.WebUI.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService _fileService;
        private readonly ICategoryService _categoryService;
        private readonly IFileTypeService _fileTypeService;
        private readonly IEducationalProgramService _educationalProgramService;
        private readonly IAccountService _accountService;

        public FileController(IFileService fileService, ICategoryService categoryService, IFileTypeService fileTypeService,
                                IEducationalProgramService educationalProgramService, IAccountService accountService)
        {
            _fileService = fileService;
            _categoryService = categoryService;
            _fileTypeService = fileTypeService;
            _educationalProgramService = educationalProgramService;
            _accountService = accountService;
        }

        
        public async Task<IActionResult> GetAllFilesByCategoryId(int id)
        {
            CategoryFileViewModel model = new CategoryFileViewModel
            {
                FileModel = new FileViewModel
                {
                    Files = await _fileService.GetAllFilesByCategoryId(id)
                },
                CategoryModel = new CategoryViewModel
                {
                    CategoryList = await _categoryService.GetAll(),
                }
            };
            if (model.FileModel.Files.Success)
            {
                model.CategoryModel.Category = new SuccessDataResult<Category>(model.CategoryModel.CategoryList.Data.FirstOrDefault(p => p.Id == id));
                return View(model);
            }
            return RedirectToAction("NotFound", "Home");
        }

        public async Task<IActionResult> ShowFileInfo(string guid)
        {
            var result = await _fileService.GetFileIdByGUID(guid);
            if(result!=null)
            {
                FileViewModel model = new FileViewModel
                {
                    FileAuthor = await _fileService.GetFileWithAuthors(result.Data)
                };
                return View(model);
            }
            return RedirectToAction("NotFound","Home");
        }

        public async Task<IActionResult> SearchByName(CategoryFileViewModel data)
        {
            var result = await _fileService.GetAllFiles();
            var filteredData = result.Data.Where(p => p.Name.ToLower().Contains(data.FileModel.Name.ToLower())).ToList();

            CategoryFileViewModel model = new CategoryFileViewModel
            {
                FileModel = new FileViewModel
                {
                    Files = new SuccessDataResult<List<File>>(filteredData)
                }
            };
            return View(model);
        }

        public async Task<IActionResult> ShowFilteredFiles(int id)
        {
            CategoryFileViewModel model = new CategoryFileViewModel
            {
                FileModel = new FileViewModel
                {
                    Files = await _fileService.GetFilesByFileTypeId(id)
                }
            };

            var fileTypes = await _fileTypeService.GetAllFileTypes();
            ViewBag.AllFileTypes = fileTypes.Data;
            return View(model);
        }

        public async Task<IActionResult> ShowFileInfoFileType(string guid)
        {
            var result = await _fileService.GetFileIdByGUID(guid);
            if (result != null)
            {
                FileViewModel model = new FileViewModel
                {
                    FileAuthor = await _fileService.GetFileWithAuthors(result.Data)
                };
                return View(model);
            }
            return RedirectToAction("NotFound", "Home");
        }

        public async Task<IActionResult> FilterByCataloq()
        {
            var model = new FileViewModel();
            return View(model);
        }

        #region ShowAllFiles (bütün kitablar)
        public async Task<IActionResult> ShowFileInfoForAllFiles(string guid)
        {
            var result = await _fileService.GetFileIdByGUID(guid);
            if (result != null)
            {
                FileViewModel model = new FileViewModel
                {
                    FileAuthor = await _fileService.GetFileWithAuthors(result.Data)
                };
                return View(model);
            }
            return RedirectToAction("NotFound", "Home");
        }

        public async Task<IActionResult> SearchByNameForShowAllFiles(CategoryFileViewModel data)
        {
            var result = await _fileService.GetAllFiles();
            var filteredData = result.Data.Where(p => p.Name.ToLower().Contains(data.FileModel.Name.ToLower())).ToList();

            CategoryFileViewModel model = new CategoryFileViewModel
            {
                FileModel = new FileViewModel
                {
                    Files = new SuccessDataResult<List<File>>(filteredData)
                }
            };
            return View(model);
        }

        public async Task<IActionResult> ShowAllFiles()
        {
            CategoryFileViewModel model = new CategoryFileViewModel
            {
                FileModel = new FileViewModel
                {
                    FileAuthors = await _fileService.GetAllFilesWithAuthors()
                }
            };
            return View(model);
        }

        #endregion

        #region Publications
        public async Task<IActionResult> ShowOurPublications()
        {
            var data = await _fileService.GetAllFilesWithAuthors();
            var result = data.Data.Where(p => p.File.EditionStatus == true).ToList();
            var model = new CategoryFileViewModel
            {
                FileModel = new FileViewModel
                {
                    FileAuthors = new SuccessDataResult<List<FileAuthorDto>>(result)
                }
            };
            return View(model);
        }
        #endregion

        #region Catalog
        public async Task<IActionResult> ECatalogIndex()
        {
            ECatalogViewModel viewModel = new ECatalogViewModel();

            var fileTypeList = await _fileTypeService.GetAllFileTypes();
            fileTypeList.Data.Add(new FileType()
            {
                Name = "Seç",
                Id = 0
            });
            viewModel.FileTypeList = new SelectList(fileTypeList.Data, "Id", "Name", "Seç");

            var categoryList = await _categoryService.GetAll();
            categoryList.Data.Add(new Category()
            {
                Name = "Seç",
                Id = 0
            });
            viewModel.CategoryList = new SelectList(categoryList.Data, "Id", "Name", "Seç");

            return View(viewModel);
        }

        public async Task<IActionResult> ECatalogFilter(ECatalogViewModel viewModel)
        {
            bool isFiltered = false;
            var filteredFiles = new List<File>();

            //FileTypeFilter
            if (viewModel.SelectedFileTypeId != 0 && !isFiltered)
            {
                var result = await _fileService.GetFilesByFileTypeId(viewModel.SelectedFileTypeId);
                filteredFiles = result.Data.Where(f => f.ExistingStatus == true).ToList();
                isFiltered = true;
            }

            //CategoryFilter
            if (viewModel.SelectedCategoryId != 0)
            {
                if (!isFiltered)
                {
                    var result = await _fileService.GetAllFiles();
                    filteredFiles = result.Data.Where(f => f.Category.Id == viewModel.SelectedCategoryId && f.ExistingStatus == true).ToList();
                    isFiltered = true;
                }
                else if (filteredFiles.Count != 0)
                {
                    filteredFiles = filteredFiles.Where(f => f.Category.Id == viewModel.SelectedCategoryId).ToList();
                }
            }

            //Author Filter
            var filteredFileAuthors = new List<FileAuthorDto>();
            bool isAuthorFiltered = false;
            if (viewModel.AuthorFilter != null)
            {
                if(isFiltered)
                {
                    if(filteredFiles?.Count > 0)
                    {
                        foreach (var file in filteredFiles)
                        {
                            var fileAuthor = await _fileService.GetFileWithAuthors(file.Id);
                            foreach (var author in fileAuthor.Data.Authors)
                            {
                                if (author.FirstName.Contains(viewModel.AuthorFilter) || author.LastName.Contains(viewModel.AuthorFilter))
                                {
                                    filteredFileAuthors.Add(fileAuthor.Data);
                                    break;
                                }
                            }
                        }
                    }
                    isAuthorFiltered = true;
                }
                else
                {
                    var result = await _fileService.GetAllFilesWithAuthors();
                    filteredFiles = new List<File>();
                    foreach (var fileauthor in result.Data)
                    {
                        foreach (var author in fileauthor.Authors)
                        {
                            if (author.FirstName.Contains(viewModel.AuthorFilter) || author.LastName.Contains(viewModel.AuthorFilter))
                            {
                                filteredFileAuthors.Add(fileauthor);
                                break;
                            }
                        }
                    }
                    isAuthorFiltered = true;
                }
            }

            //BookFilter
            if (viewModel.BookFilter != null)
            {
                if(isAuthorFiltered)
                {
                    foreach (var fileAuhor in filteredFileAuthors)
                    {
                        filteredFileAuthors = filteredFileAuthors.Where(fa => fa.File.Name.Contains(viewModel.BookFilter)).ToList();
                    }
                }
                else if (isFiltered)
                {
                    if(filteredFiles?.Count != 0)
                        filteredFiles = filteredFiles.Where(f => f.Name.Contains(viewModel.BookFilter)).ToList();
                }
                else
                {
                    var result = await _fileService.GetAllFiles();
                    filteredFiles = result.Data.Where(f => f.Name.Contains(viewModel.BookFilter) && f.ExistingStatus == true).ToList();
                    isFiltered = true;
                }
            }

            //PublicationMin/Max Date Filter
            if (viewModel.PublicationYearMin != 0)
            {
                if(isAuthorFiltered)
                {
                    filteredFileAuthors = filteredFileAuthors.Where(fa => fa.File.PublicationDate.Year >= viewModel.PublicationYearMin).ToList();
                }
                else if (isFiltered)
                {
                    if(filteredFiles?.Count != 0)
                        filteredFiles = filteredFiles.Where(f => f.PublicationDate.Year >= viewModel.PublicationYearMin).ToList();
                }
                else
                {
                    var result = await _fileService.GetAllFiles();
                    filteredFiles = result.Data.Where(f => f.PublicationDate.Year >= viewModel.PublicationYearMin).ToList();
                    isFiltered = true;
                }
            }
            if (viewModel.PublicationYearMax != 0)
            {
                if (isAuthorFiltered)
                {
                    filteredFileAuthors = filteredFileAuthors.Where(fa => fa.File.PublicationDate.Year <= viewModel.PublicationYearMax).ToList();
                }
                else if (isFiltered)
                {
                    if(filteredFiles?.Count != 0)
                        filteredFiles = filteredFiles.Where(f => f.PublicationDate.Year <= viewModel.PublicationYearMax).ToList();
                }
                else
                {
                    var result = await _fileService.GetAllFiles();
                    filteredFiles = result.Data.Where(f => f.PublicationDate.Year <= viewModel.PublicationYearMax).ToList();
                    isFiltered = true;
                }
            }

            if (!isAuthorFiltered && isFiltered)
            {
                if(filteredFiles?.Count>0)
                {
                    var fileAuthor = await _fileService.GetFileWithAuthors(filteredFiles[0].Id);
                    foreach (var file in filteredFiles)
                    {
                        fileAuthor = await _fileService.GetFileWithAuthors(file.Id);
                        filteredFileAuthors?.Add(fileAuthor.Data);
                    }
                }
            }

            viewModel.FilteredFileAuthors = filteredFileAuthors;

            var fileTypeList = await _fileTypeService.GetAllFileTypes();
            fileTypeList.Data.Add(new FileType()
            {
                Name = "Seç",
                Id = 0
            });
            viewModel.FileTypeList = new SelectList(fileTypeList.Data, "Id", "Name");

            var categoryList = await _categoryService.GetAll();
            categoryList.Data.Add(new Category()
            {
                Name = "Seç",
                Id = 0
            });
            viewModel.CategoryList = new SelectList(categoryList.Data, "Id", "Name");

            return View(viewModel);
        }

        public async Task<IActionResult> ShowFileInfoCatalog(string guid)
        {
            var result = await _fileService.GetFileIdByGUID(guid);
            if (result != null)
            {
                FileViewModel model = new FileViewModel
                {
                    FileAuthor = await _fileService.GetFileWithAuthors(result.Data)
                };
                return View(model);
            }
            return RedirectToAction("NotFound", "Home");
        }
        #endregion

        #region Educational Programs
        public async Task<IActionResult> EducationalPrograms()
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var email = HttpContext.Session.GetString("Email");

            var educationalPrograms = await _educationalProgramService.GetAll(accessToken);

            var allFileTypes = await _fileTypeService.GetAllFileTypes();
            ViewBag.AllFileTypes = allFileTypes.Data;

            var account = await _accountService.GetByEmail(accessToken, email);
            AccountViewModel viewModel = new AccountViewModel()
            {
                EducationalProgramViewModel = new EducationalProgramViewModel()
                {
                    EducationalPrograms = educationalPrograms.Data
                },
                Account =account.Data
            };
            return View(viewModel);
        }

        public async Task<IActionResult> ShowEducationalProgramInfo(string guid)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            var email = HttpContext.Session.GetString("Email");
            var account = await _accountService.GetByEmail(accessToken, email);

            var allFileTypes = await _fileTypeService.GetAllFileTypes();
            ViewBag.AllFileTypes = allFileTypes.Data;

            var educationalProgram = await _educationalProgramService.GetByGUID(accessToken , guid);
            if (educationalProgram != null)
            {
                AccountViewModel viewModel = new AccountViewModel()
                {
                    EducationalProgramViewModel = new EducationalProgramViewModel()
                    {
                        EducationalProgram = educationalProgram.Data
                    },
                    Account = account.Data
                };
                return View(viewModel);
            }
            return RedirectToAction("NotFound", "Home");
        }
        #endregion
    }
}
