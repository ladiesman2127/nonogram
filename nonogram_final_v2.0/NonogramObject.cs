[Serializable]
public class NonogramObject
{
	public string    Name = null!;
	public int       Width;
	public int       Height;
	public List<int> Lst = null!;

	public NonogramObject(string name, int width, int height, List<int> lst)
	{
		Name   = name;
		Width  = width;
		Height = height;
		Lst    = lst;
	}
	public NonogramObject() { }
}