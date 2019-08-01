using System;

namespace JuniorStart.DTO
{
    public class RecruitmentInformationDto
    {
        /// <summary>
        /// Recruitment id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// company name where you apply
        /// </summary>
        /// <example>Orange</example>
        public string CompanyName { get; set; }

        /// <summary>
        /// city where you apply
        /// </summary>
        /// <example>Poznań</example>
        public string City { get; set; }

        /// <summary>
        /// the name of the position you are applying for
        /// </summary>
        /// <example>.NET Developer</example>
        public string WorkPlace { get; set; }

        /// <summary>
        /// date of company reply
        /// </summary>
        /// <example>2019-08-09 16:05:07</example>
        public DateTime DateOfCompanyReply { get; set; }
        
        /// <summary>
        /// Application date
        /// </summary>
        /// <example>2019-08-19 12:12:00</example>
        public DateTime ApplicationDate { get; set; }

        /// <summary>
        /// Is company reply
        /// </summary>
        public bool CompanyReply { get; set; }

        /// <summary>
        /// Notes to recruitment
        /// </summary>
        /// <example>Good interview</example>
        public string Notes { get; set; }

        /// <summary>
        /// Link to apply
        /// </summary>
        /// <example>https://allegro.pl</example>
        public string LinkToApplication { get; set; }

        /// <summary>
        /// the recruiter id
        /// </summary>
        /// <example>1</example>
        public int OwnerId { get; set; }
    }
}