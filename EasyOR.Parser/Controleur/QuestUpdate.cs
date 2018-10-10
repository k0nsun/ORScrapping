using EasyOR.Parser.View;
using System.Windows.Forms;
using System.Collections;
using EasyOR.DTO;
using EasyOR.DataAccess.SqlServer;
using System.Collections.Generic;

namespace EasyOR.Parser.Controleur
{
    public class QuestUpdate
    {
        private const string urlAllQuests = "http://universphoenix.origins-return.fr/quete.php?cat=list";
      
        public void GetAllQuest(Main mainForm)
        {            
            if (new Navigation().NavigationPage(mainForm.webBrowserMain, urlAllQuests))
            {
                HtmlDocument htmlDoc = mainForm.webBrowserMain.Document;               
                HtmlElementCollection listTable = htmlDoc.GetElementsByTagName("table");
                foreach (HtmlElement linetable in listTable[1].Children[0].Children)
                {
                    var questName = linetable.InnerText;
                    QuestAction questAction = new QuestAction();
                    List<Quest> listQuest = (List<Quest>)questAction.GetQuestByName(questName);
                    if(listQuest.Count == 0)
                    {
                        Quest quest = new Quest();
                        quest.Name = questName;
                        quest.IsCheckName = true;
                        questAction.AddQuest(quest);
                    }
                    else if (listQuest.Count > 1)
                    {
#if _DEV
                        System.Diagnostics.Debug.Write(string.Format("Error add/update quest: {0}", questName));
#endif
                    }
                }
                mainForm.action = Action.UNKNOW;
            }           
        }
    }
}
