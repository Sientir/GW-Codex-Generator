namespace GW_Codex_Generator.Challenge_UI
{
    interface IChallengeToHTML
    {
        string GetHTML();
        void SetHTMLRefreshFunction(Challenges.RefreshHTMLFunction refreshFunction);
    }
}
