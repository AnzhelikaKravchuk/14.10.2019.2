using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable.Tests
{
    public class Book : IEquatable<Book>, IComparable<Book>
    {
        public Book()
        {
        }

        public Book(string isbn, string author, string title)
        {
            Isbn = isbn;
            Author = author;
            Title = title;
        }

        public Book(string isbn, string author, string title, double price) : this(isbn, author, title)
        {
            Price = price;
        }

        public Book(string isbn, string author, string title, string publishingHouse, int year, int numberOfPages, double price)
        {
            Isbn = isbn;
            Author = author;
            Title = title;
            PublishingHouse = publishingHouse;
            Year = year;
            NumberOfPages = numberOfPages;
            Price = price;
        }

        public string Isbn { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string PublishingHouse { get; set; }

        public int Year { get; set; }

        public int NumberOfPages { get; set; }

        public double Price { get; set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="firstBook">The first book.</param>
        /// <param name="secondBook">The second book.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Book firstBook, Book secondBook) => firstBook.Equals(secondBook);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="firstBook">The first book.</param>
        /// <param name="secondBook">The second book.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Book firstBook, Book secondBook) => !firstBook.Equals(secondBook);

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="other" /> in the sort order.  Zero This instance occurs in the same position in the sort order as <paramref name="other" />. Greater than zero This instance follows <paramref name="other" /> in the sort order.
        /// </returns>
        public int CompareTo(Book other)
        {
            if (other is null)
            {
                return 1;
            }

            if (GetType() != other.GetType())
            {
                return 1;
            }

            return Isbn.CompareTo(other.Isbn);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(Book other)
        {
            if (other is null)
            {
                return false;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return Isbn.Equals(other.Isbn);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Isbn},{Author},{Title},{PublishingHouse},{Year},{NumberOfPages},{Price}";

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals(obj as Book);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => ToString().GetHashCode();
    }
}
