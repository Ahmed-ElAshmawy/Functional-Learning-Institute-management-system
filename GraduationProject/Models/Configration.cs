using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GraduationProject.Models.dto;

namespace GraduationProject.Models
{
    public class Configration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                //Therapist Map

                cfg.CreateMap<CreateTherapistDto, Therapist>()
                .ForMember(prop => prop.Id, opt => opt.Ignore());

                cfg.CreateMap<BaseTherapistDto, Therapist>().ReverseMap();

                cfg.CreateMap<Therapist, TherapistDto>()
                .ForMember(prop => prop.Rates,
                opt => opt.MapFrom(src => src.Rates.Select(ch => new RateDto(ch.Date, ch.BCValue, ch.OtherValue, ch.TherapistId))));

                cfg.CreateMap<TherapistDto, Therapist>()
                .ForMember(prop => prop.Rates,
                opt => opt.MapFrom(src => src.Rates.Select(ch => new Rate { Date = ch.Date, BCValue = ch.BCValue, OtherValue = ch.OtherValue, TherapistId = ch.TherapistId })));

                cfg.CreateMap<CreateTherapistDto, Therapist>()
                .ForMember(prop => prop.Id, opt => opt.Ignore());

                //Child Map
                cfg.CreateMap<Child, ChildDto>()
                    .ForMember(prop => prop.Amount,
                        opt => opt.MapFrom(src => src.ClientFunds.OrderBy(x => x.EndTime).Last().Amount))
                    .ForMember(prop => prop.BillingNumber,
                        opt => opt.MapFrom(src => src.ClientFunds.OrderBy(ch => ch.StartTime).Last().BillingNumber))
                    .ForMember(prop => prop.Remaining,
                        opt => opt.MapFrom(src =>  src.ClientFunds.OrderBy(ch => ch.StartTime).Last().Remaining))
                    .ForMember(prop => prop.Used,
                        opt => opt.MapFrom(src => src.ClientFunds.OrderBy(ch => ch.StartTime).Last().Used));


                cfg.CreateMap<ChildDto, Child>();
                cfg.CreateMap<Slot, SlotDto>().ReverseMap();
                cfg.CreateMap<Rate, RateDto>();
                cfg.CreateMap<RateDto, Rate>().
                ForMember(prop => prop.Id, opt => opt.Ignore());
                //ParentEmail
                cfg.CreateMap<ParentEmail, ParentEmailDto>();
                cfg.CreateMap<ParentEmailDto, ParentEmail>()
                .ForMember(prop => prop.Id, opt => opt.Ignore());
                //TherapistTask
                cfg.CreateMap<TherapistTask, TherapistTaskDto>().ReverseMap();
                //TherapistAvailability
                cfg.CreateMap<TherapistAvailabilityDto, TherapistAvailability>()
                     .ForMember(prop => prop.Id, opt => opt.Ignore());
                cfg.CreateMap<TherapistAvailability, TherapistAvailabilityDto>();

                //ChildrenAvailability
                cfg.CreateMap<ChildrenAvailabilityDto, ChildrenAvailability>()
                    .ForMember(prop => prop.Id, opt => opt.Ignore());
                cfg.CreateMap<ChildrenAvailability, ChildrenAvailabilityDto>();


                //Session Map
                cfg.CreateMap<Session, SessionDto>()
                    .ForMember(prop => prop.Children,
                        opt => opt.MapFrom(src => src.Children.Select(ch => new GList(ch.Id, ch.FirstName + " " + ch.LastName))))
                        .ForMember(prop => prop.TherapistName,
                        opt => opt.MapFrom(src => src.Therapist.FullLegalName));

                cfg.CreateMap<SessionDto, Session>()
                 .ForMember(prop => prop.Id, opt => opt.Ignore())
                .ForMember(prop => prop.Children,
                 opt => opt.MapFrom(src => src.Children.Select(ch => new Child { Id = ch.Id })))
                 .ForMember(prop => prop.Slots,
                 opt => opt.MapFrom(src => src.Slots.Select(ch => new Slot { Id = ch.Id })));

                //Parent Map
                cfg.CreateMap<Parent, ParentDto>()
              .ForMember(prop => prop.Children,
               opt => opt.MapFrom(src => src.Children.Select(ch => new GList(ch.Id, ch.FirstName))))
                  .ForMember(prop => prop.Emails,
               opt => opt.MapFrom(src => src.Emails.Select(ch => new ParentEmailDto(ch.ParentId, ch.Email))))
               .ForMember(prop => prop.Contacts,
               opt => opt.MapFrom(src => src.Contacts.Select(ch => new ContactDto(ch.ParentId, ch.Number, ch.ContactName, ch.ContactType))));

                cfg.CreateMap<CreateParentDto, Parent>()
               .ForMember(prop => prop.Id, opt => opt.Ignore())
               .ForMember(prop => prop.UserName, opt => opt.MapFrom(src => src.Email))
                 .ForMember(prop => prop.Emails,
                    opt => opt.MapFrom(src => new List<ParentEmail>
                    {
                         new ParentEmail {Email = src.Email }
                    }))
                       .ForMember(prop => prop.Contacts,
                       opt => opt.MapFrom(src => new List<Contact>
                       {
                            new Contact {ContactName = src.EmergancyContactName,Number = src.EmergancyContactNumber,ContactType = ContactType.Emergancy} ,
                            new Contact {ContactName = src.CellContactName,Number = src.CellContactNumber,ContactType = ContactType.Home}
                       }));

                cfg.CreateMap<ParentDto, Parent>()
                               .ForMember(prop => prop.Id, opt => opt.Ignore())
               .ForMember(prop => prop.Children,
                opt => opt.MapFrom(src => src.Children.Select(ch => new Child { Id = ch.Id })))
                   .ForMember(prop => prop.Emails,
                opt => opt.MapFrom(src => src.Emails.Select(ch => new ParentEmail { ParentId = ch.ParentId, Email = ch.Email })))
                .ForMember(prop => prop.Contacts,
                opt => opt.MapFrom(src => src.Contacts.Select(ch => new Contact { ParentId = ch.ParentId, Number = ch.Number, ContactType = ch.ContactType, ContactName = ch.ContactName })));
                cfg.CreateMap<BaseParentDto, Parent>().ReverseMap();
                cfg.CreateMap<ContactDto, Contact>().ForMember(prop => prop.Id, opt => opt.Ignore());
                cfg.CreateMap<Contact, ContactDto>();

                cfg.CreateMap<Complaint, ComplaintDto>().ForMember(prop => prop.UserName,
                    opt => opt.MapFrom(src => src.User.UserName));


                cfg.CreateMap<ComplaintDto, Complaint>().ForMember(prop => prop.Id, opt => opt.Ignore());

                cfg.CreateMap<UserMessage, UserMessageDto>()
               .ForMember(prop => prop.UserName, opt => opt.MapFrom(src => src.From.UserName));
            });
        }
    }
}