using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW_Codex_Generator.Codex_Method_Settings
{
    interface ICodexPoolSettingsGenerator
    {
        List<Skill> GenerateCodex(List<Skill> pool);
    }
}
