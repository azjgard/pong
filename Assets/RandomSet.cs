public class RandomSet<T> {
    private T[] items;
    private System.Random rnd;

    public RandomSet(T[] _items) {
        items = _items;
        rnd = new System.Random();
    }

    public T Next() {
        int i = rnd.Next(0, items.Length);
        return items[i];
    }
}