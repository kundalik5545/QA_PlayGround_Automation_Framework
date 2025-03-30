using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QA_PlayGround.Base_Class;
using QA_PlayGround.POM_Class;

namespace QA_PlayGround.TestClass
{
    public class Input_Test : BaseTest
    {
        Home_Page? homePage;

        [Test]
        public void Verify_EnterMoviewName()
        {
            homePage = new Home_Page(GetDriver());

            homePage!
                .NavigateTo_PracticePage()
                .NavigateTo_InputPage()
                .Enter_MovieName("IronMan");

            Thread.Sleep(3000);
        }

        [Test] 
        public void AppendText_AndPress_TabKeys()
        {
            homePage = new Home_Page(GetDriver());

            homePage!
                .NavigateTo_PracticePage()
                .NavigateTo_InputPage()
                .Enter_AppendNewText_PressTab("Hello");

            Thread.Sleep(3000);
        }
    }
}
