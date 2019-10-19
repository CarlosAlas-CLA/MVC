namespace MVC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;
    public class ContactsModel : DbContext
    { 


        public ContactsModel()
            : base("name=ContactsInformation")
        {
        }
        public virtual DbSet<Contact> MyEntitiesContact { get; set; }
        public virtual DbSet<EmailAddress> MyEntitiesEmailAddress { get; set; }
    }
    public class Contact
    {
        [Key]
        public int ContactID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<EmailAddress> EmailAddresses { get; set; }
    }
    public class EmailAddress
    {
        [Key]
        public int EmailID { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        public EmailType EmailType { get; set; }
        [ForeignKey("Contact")]
        public int ContactID { get; set; }
        public virtual Contact Contact { get; set; }
    }
    public enum EmailType
    {
        Personal, Business
    }
}
