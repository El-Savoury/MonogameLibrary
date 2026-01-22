namespace MonogameLibrary.Utilities
{
    public class WeightedPicker<T>
    {
        private readonly Dictionary<T, float> weightedItems = [];
        private float totalWeight = 0.0f;


        public WeightedPicker() { }


        public void Add(T item, float weight)
        {
            if (weight <= 0.0f)
            {
                throw new InvalidOperationException("Weight must be a non-negative value");
            }

            weightedItems.Add(item, weight);
            totalWeight += weight;
        }


        public T Pick(Random random)
        {
            if (weightedItems.Count == 0)
            {
                throw new InvalidOperationException("Contains no items to pick from");
            }

            float rng = random.NextSingle() * totalWeight;
            float cumulativeWeight = 0.0f;

            foreach (T item in weightedItems.Keys)
            {
                float itemWeight = weightedItems[item];
                cumulativeWeight += itemWeight;

                if (rng <= cumulativeWeight)
                {
                    return item;
                }
            }

            // Default to last item if we somehow fail to pick due to float precision issues
            return weightedItems.Last().Key;
        }


        public void Clear()
        {
            weightedItems.Clear();
            totalWeight = 0.0f;
        }
    }
}
