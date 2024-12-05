using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Library.Enums;

namespace Library.Entities
{
    public class EBook : Book
    {
        private string _previewLink;

        public FileFormat FileFormat { get; set; }

        public bool DRMProtection { get; set; }

        public string PreviewLink
        {
            get => _previewLink;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Preview link cannot be null or empty.");

                var urlPattern =
                    @"^(https?|ftp):\/\/[a-zA-Z0-9\-\.]+(\.[a-zA-Z]{2,})+(\/[a-zA-Z0-9\-\.]*)*(\?[a-zA-Z0-9&%=]*)?$";

                if (!Regex.IsMatch(value, urlPattern))
                    throw new ArgumentException("Preview link must be a valid URL.");

                _previewLink = value;
            }
        }

        public int PurchasedCount { get; private set; }

        public override string Details => $"{base.Details}\nFile Format: {FileFormat}\nDRM Protected: {DRMProtection}\nPreview Link: {PreviewLink}\nNumber of purchased: {PurchasedCount}";

        public EBook(
            string title,
            string description,
            List<BookGenre> genres,
            List<Author> authors,
            string isbn,
            string previewLink,
            FileFormat fileFormat = FileFormat.OTHER
        )
            : base(title, description, genres, authors, isbn)
        {
            PreviewLink = previewLink;
            FileFormat = fileFormat;
            DRMProtection = false;
        }

        public override void GetBook(Visitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException(nameof(visitor), "Visitor cannot be null.");

            visitor.TakeBook(this);

            PurchasedCount++;

            OnPropertyChanged(nameof(Details));
        }

        public override string ToString()
        {
            string authors = string.Join(" ", Authors.Select(a => a.FullName));
            string genres = string.Join(" ", Genres);

            return $"{Title} {genres} {authors} {ISBN} {FileFormat} {DRMProtection} {PreviewLink}";
        }
    }
}
