using System.ComponentModel;

namespace DataUtilityCore.Enum
{
    public enum EnumTypeofRaccomandata
    {
        TypeAccertamento = 1,
        TypeRevocaAccertamento = 2,
        [Description("Conferma pianificazione ispezione")]
        TypePianificazioneIspezioneConferma = 3,
        [Description("Annullamento pianificazione ispezione")]
        TypePianificazioneIspezioneAnnulla = 4,
        [Description("Ripianificazione ispezione")]
        TypePianificazioneIspezioneRipianificazione = 5,
        [Description("Notifica Sanzione")]
        TypeSanzione = 6,
        [Description("Libere")]
        TypeLibere = 7,
        [Description("Revoca Sanzione")]
        TypeRevocaSanzione = 8
    }
}
