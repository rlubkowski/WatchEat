using WatchEat.Helpers.Attributes;

namespace WatchEat.Enums
{
    public enum BMI : int
    {
        [LocalizedDescription(nameof(VerySeverelyUnderweight))]
        VerySeverelyUnderweight,    //Very severely underweight   less than 15.0
        [LocalizedDescription(nameof(SeverelyUnderweight))]
        SeverelyUnderweight,        //Severely underweight	15.0 – 16.0
        [LocalizedDescription(nameof(Underweight))]
        Underweight,                //Underweight	16.0 – 18.5
        [LocalizedDescription(nameof(HealthyWeight))]
        HealthyWeight,              //Healthy weight	18.5 – 25
        [LocalizedDescription(nameof(Overweight))]
        Overweight,                 //Overweight	25.0 – 30.0
        [LocalizedDescription(nameof(ModeratelyObese))]
        ModeratelyObese,            //Moderately obese	30.0 – 35.0
        [LocalizedDescription(nameof(SeverelyObese))]
        SeverelyObese,              //Severely obese	35.0 – 40.0
        [LocalizedDescription(nameof(VerySeverelyObese))]
        VerySeverelyObese           //Very severely obese more than 40.0
    }
}
