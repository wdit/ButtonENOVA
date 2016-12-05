using Soneta.Business;
using Soneta.Core;
using Soneta.Types;
using Soneta.Tools;
using Soneta.Business.UI;
using System.Linq;
using MyNamespace;
using Soneta.Business.Db.Permissions;
using System.Collections.Generic;
using ButtonExample;

[assembly: Worker(typeof(MyNamespace.MyWorker), typeof(DokEwidencji))]

//[assembly: FolderView("Dkret Dekretów", FolderType =typeof(DokEwidencji), BrickColor = FolderViewAttribute.BlackBrick)]  
namespace MyNamespace
{
   public class MyWorker
    {
        public static object dokNumber;
        public static object dekDok;
        public class MyParams : ContextBase
        {
            
            public MyParams(Context ctx) : base(ctx) { }

            //[Caption("Numer dokumentu"), Priority(1)]
            //public string FirstParameter
            //{
            //    get { return dokNumber; }
            //    set { dokNumber = value;  }
            //}

        }

        //[Context]
        //public MyParams Prms { get; set; }

        [Context]
        public DokEwidencji Dok { get; set; }

        [Action("Dekrety",
        Priority = 10,
        Icon = ActionIcon.Wizard,
        Target = ActionTarget.ToolbarWithText,
        Mode = ActionMode.SingleSession  | ActionMode.OnlyTable)]
        public FormActionResult MyAction()
        {
            //object towar = Dok.Session.Tables["Kontrahent"][7];
            object cos = Dok.Session.Tables["CentrumKosztow"];
            dokNumber = Dok.NumerDokumentu;
            dekDok = Dok.Dekrety;
            

            var page2 = new PageContainer
            {
                Width = "*",
                Height = "*",
                //DataContext = dokNumber.ToString()
            };

            var group1 = new GroupContainer { CaptionHtml = "Numer dokumentu"  };
            page2.Elements.Add(group1);
            group1.Elements.Add(new FieldElement
            {
                EditValue = "{Nazwa1}"
            });

            var group2 = new GroupContainer { CaptionHtml="XXX" };
            page2.Elements.Add(group2);
            group2.Elements.Add(new GridElement
            {
                CaptionHtml = " "+dekDok,
                EditValue = "{Nazwa}"                                
            });

            var form1 = new Soneta.Business.UI.DataForm();
            form1.Elements.Add(page2);


            //list1.Elements.Add(page2);
           
            return new FormActionResult()
            {
                EditValue = Dok.NumerDokumentu,               
                Pages = new[]
                      {                                 
                         new FormPageInfo("DokEwidencji.ElementDziennikaKsiegowegoDekrety.pageform.xml"),
                         new FormPageInfo("DekretBase.DekretOgolne.pageform.xml")
                         //new FormPageInfo(form1)
                      }                
            };
        }

        public static bool IsEnabledMyAction(DokEwidencji dok)
        {
            return true;
        }
    }
}