using System.Collections.Generic;

namespace GW_Codex_Generator.Codex_Method_Settings
{
    interface ICodexPoolSettingsGenerator
    {
        List<Skill> GenerateCodex(List<Skill> pool);
    }
}
