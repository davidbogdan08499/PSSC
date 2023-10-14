namespace LAB1.Domain
{

    public record Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return $"Person: {FirstName}, {LastName}, {Email}, {Address},{Phone}";
        }

    }
}