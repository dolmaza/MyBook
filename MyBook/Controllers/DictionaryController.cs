using Core;
using Core.Properties;
using DevExpress.Web.Mvc;
using MyBook.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyBook.Controllers
{
    public class DictionaryController : BaseController
    {
        [Route("dictionaries", Name = "Dictionaries")]
        public ActionResult Index()
        {
            var model = new DictionaryViewModel
            {
                TreeViewModel = GetTreeViewModel()
            };
            return View(model);
        }

        [Route("dictionaries/tree", Name = "DictioanriesTree")]
        public ActionResult DictionaryTree()
        {
            return PartialView("_DictionaryTree", GetTreeViewModel());
        }

        [Route("dictionaries/add", Name = "DictionariesAdd")]
        public ActionResult DictioanriesAdd([ModelBinder(typeof(DevExpressEditorsBinder))] DictionaryTreeItem model)
        {
            UnitOfWork.DictionaryRepository.Add(new Dictionary
            {
                ID = model.ID,
                ParentID = model.ParentID,
                Caption = model.Caption,
                CaptionEng = model.CaptionEng,
                StringCode = model.StringCode,
                IntCode = model.IntCode,
                DecimalValue = model.DecimalValue,
                Code = model.Code,
                SortIndex = model.SortIndex
            });

            UnitOfWork.Complate();

            if (UnitOfWork.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_DictionaryTree", GetTreeViewModel());
        }

        [Route("dictionaries/update", Name = "DictionariesUpdate")]
        public ActionResult DictioanriesUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] DictionaryTreeItem model)
        {
            UnitOfWork.DictionaryRepository.Update(new Dictionary
            {
                ID = model.ID,
                ParentID = model.ParentID,
                Caption = model.Caption,
                CaptionEng = model.CaptionEng,
                StringCode = model.StringCode,
                IntCode = model.IntCode,
                DecimalValue = model.DecimalValue,
                Code = model.Code,
                SortIndex = model.SortIndex
            });

            UnitOfWork.Complate();

            if (UnitOfWork.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_DictionaryTree", GetTreeViewModel());
        }

        [Route("dictionaries/delete", Name = "DictionariesDelete")]
        public ActionResult DictionarisDelete(int? ID)
        {
            var dictionary = UnitOfWork.DictionaryRepository.Get(ID);
            UnitOfWork.DictionaryRepository.Remove(dictionary);

            UnitOfWork.Complate();

            if (UnitOfWork.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_DictionaryTree", GetTreeViewModel());
        }

        private DictionaryTreeViewModel GetTreeViewModel()
        {
            return new DictionaryTreeViewModel
            {
                ListUrl = Url.RouteUrl("DictioanriesTree"),
                AddNewUrl = Url.RouteUrl("DictionariesAdd"),
                UpdateUrl = Url.RouteUrl("DictionariesUpdate"),
                DeleteUrl = Url.RouteUrl("DictionariesDelete"),
                TreeItems = UnitOfWork.DictionaryRepository.GetAll().Select(d => new DictionaryTreeItem
                {
                    ID = d.ID,
                    ParentID = d.ParentID,
                    Caption = d.Caption,
                    CaptionEng = d.CaptionEng,
                    StringCode = d.StringCode,
                    IntCode = d.IntCode,
                    DecimalValue = d.DecimalValue,
                    Code = d.Code,
                    SortIndex = d.SortIndex
                }).ToList()
            };
        }
    }
}