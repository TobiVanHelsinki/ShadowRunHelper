using System;
using System.Collections.Generic;
using Windows.ApplicationModel;

namespace ShadowRunHelper
{
    internal enum TCharState
    {
        EMPTY_CHAR = 0,
        LOAD_CHAR = 1,
        NEW_CHAR = 2,
    }
    internal enum ThingDefs
    {
        UndefTemp = -2,
        Undef = -1,

        Handlung = 1,
        Fertigkeit = 2,
        Item = 3,
        Programm = 4,
        Munition = 5,
        Implantat = 6,
        Vorteil = 7,
        Nachteil = 8,
        Connection = 9,
        Sin = 10,
        Attribut = 11,
        Nahkampfwaffe = 12,
        Fernkampfwaffe = 13,
        Kommlink = 14,
        CyberDeck = 15,
        Vehikel = 16,
        Panzerung = 17,
        Eigenschaft = 18,
    }

    internal enum FolderMode
    {
        Intern, Extern
    }
}
