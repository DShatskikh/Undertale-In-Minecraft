using PixelCrushers;
using YG;

namespace Game
{
    public class YandexGameSavedGameDataStorer : SavedGameDataStorer
    {
        public override bool HasDataInSlot(int slotNumber) => 
            slotNumber == 1;

        public override void StoreSavedGameData(int slotNumber, SavedGameData savedGameData)
        {
            var s = SaveSystem.Serialize(savedGameData);
            print(s);
            YandexGame.savesData.SavedGameData = s;
            YandexGame.SaveProgress();
        }

        public override SavedGameData RetrieveSavedGameData(int slotNumber)
        {
            //return YandexGame.savesData.SavedGameData;
            var s = YandexGame.savesData.SavedGameData;
            return HasDataInSlot(slotNumber) ? SaveSystem.Deserialize<SavedGameData>(s) : new SavedGameData();
        }

        public override void DeleteSavedGameData(int slotNumber)
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }
    }
}