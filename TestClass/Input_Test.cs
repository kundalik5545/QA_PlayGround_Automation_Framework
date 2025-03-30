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

        [Test]
        public void VerifyText_InsideInputField()
        {
            homePage = new Home_Page(GetDriver());

            string actaulText = homePage!
                                  .NavigateTo_PracticePage()
                                  .NavigateTo_InputPage()
                                  .Verify_TextInside_Input();

            Console.WriteLine($"Actual text is -{actaulText}");

            Assert.That(actaulText, Is.EqualTo("QA PlayGround"));

            Thread.Sleep(3000);
        }
    }
}
