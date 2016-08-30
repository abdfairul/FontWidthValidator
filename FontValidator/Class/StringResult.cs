namespace FontValidator
{
    public class StringResult //one instance for every langunage in every id
    {
        public string label;  // language

        public string value_string; // the string
        public float calc_base_width;
        public float calc_base_height;
        public double calc_w_tolerance_width;
        public double calc_w_tolerance_height;
        public double calc_row_count;
        public bool is_ok_width;
        public bool is_ok_height;

        
    }
}
