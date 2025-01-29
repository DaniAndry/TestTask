namespace Items
{
    public interface IItem
    {
        public bool Taken { get;}
        public void Take();
        public void Drop();
    }
}