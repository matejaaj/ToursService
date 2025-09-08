using System;
using System.Collections.Generic;

namespace Tours.Core.Domain.Entities.Tour
{
    public class TourReview : Entity
    {
        public long TouristId { get; private set; }
        public long TourId { get; private set; }
        public int Rating { get; private set; }
        public string Comment { get; private set; }
        public DateTime TourDate { get; private set; }
        public DateTime ReviewDate { get; private set; }
        public List<string> Images { get; private set; }
        public double Completion { get; private set; }

        public TourReview(long touristId, long tourId, int rating, string comment, DateTime tourDate, DateTime reviewDate, List<string> images, double completion)
        {
            TouristId = touristId;
            TourId = tourId;
            Rating = rating;
            Comment = comment;
            TourDate = tourDate;
            ReviewDate = reviewDate;
            Images = images ?? new List<string>();
            Completion = completion;

            Validate();
        }


        public void Validate()
        {
            ValidateRating();
            ValidateComment();
            ValidateTourDate();
            ValidateReviewDate();
            ValidateCompletion();
        }



        private void ValidateRating()
        {
            if (Rating < 1 || Rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5.");
        }

        private void ValidateCompletion()
        {
            if (Completion < 35 || Completion > 100)
                throw new ArgumentException("Completion must be between 35% and 100%.");
        }


        private void ValidateComment()
        {
            if (string.IsNullOrWhiteSpace(Comment))
                throw new ArgumentException("Comment cannot be blank.");
        }

        private void ValidateTourDate()
        {
            if (TourDate > DateTime.Now)
                throw new ArgumentException("Tour date cannot be in the future.");
        }

        private void ValidateReviewDate()
        {
            if (ReviewDate < TourDate)
                throw new ArgumentException("Review date must be greater than or equal to the tour date.");
        }

        public void SetCompletion(double completion)
        {
            Completion = completion;
            Validate();
        }
    }
}
