namespace Client.Models.Enums;

public enum Gender
{
	Female = 0,
	Male = 1
}

public static class GenderExtensions
{
	public static bool? FromGender(this Gender gender)
	{
		return gender switch
		{
			Gender.Female => false,
			Gender.Male => true,
			_ => null
		};
	}

	public static Gender FromBool(this bool? gender)
	{
		return gender switch
		{
			false => Gender.Female,
			true => Gender.Male,
			_ => Gender.Male,
		};
	}
}
