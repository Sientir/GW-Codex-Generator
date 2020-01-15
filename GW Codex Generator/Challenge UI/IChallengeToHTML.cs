using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW_Codex_Generator.Challenge_UI
{
    interface IChallengeToHTML
    {
        string GetHTML();
        void SetHTMLRefreshFunction(Challenges.RefreshHTMLFunction refreshFunction);
    }
}
