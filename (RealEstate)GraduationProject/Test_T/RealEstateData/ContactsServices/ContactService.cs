using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstate.Core;
using RealEstate.Core.Entities.ContactEntity;
using RealEstate.Data.IRepos;
using RealEstate.Data.SqlDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Data.Contacts
{
    public class ContactService : IRealEstateRepo<Contact>
    {
        private DBData dB;
        Contact contact;
        public ContactService()
        {
            dB = new DBData();
        }

        public void Add(Contact table)
        {
            dB.Contact.Add(table);
            dB.SaveChanges();
        }

        public void Delete(int id)
        {
            contact = Find(id);
            dB.Contact.Remove(contact);
            dB.SaveChanges();

        }

        public List<Contact> GetData()
        {
            return dB.Contact.ToList();
        }

        public List<Contact> SearchItem(string searchItem)
        {
            return dB.Contact
                .Where(a => a.Id.ToString().Equals(searchItem)
                    || a.Name.Contains(searchItem)
                    || a.Number.ToString().Contains(searchItem)
                    || a.Email.Contains(searchItem))
                .ToList();
        }


        public void Update(Contact table, int id)
        {
            contact = Find(id);
            dB.Contact.Update(contact);
            dB.SaveChanges();

        }

        public Contact Find(int id)
        {
            return dB.Contact.Where(x => x.Id == id).First();
        }

        public List<Contact> GetDataByUserId(string UserId)
        {
            if (dB.Database.CanConnect())
            {
                return dB.Contact.Where(x => x.UserId == UserId).ToList();
            }
            else
            {
                return null;
            }
        }

        public List<Contact> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int PostsCounts(string UserId)
        {
            throw new NotImplementedException();
        }
    }
}
