using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Component;
using BLL.BusinessObject;

namespace BLL.Component
{
    class ManufacturerBLL
    {
        public void Add(ref Manufacturer vobjManufacturer)
        {
            try
            {
                new ManufacturerDB().Add(ref vobjManufacturer);
            }
            catch (Exception ex)
            {
                
                //TODO: Handle exception
            }

        }

        public void Update(ref Manufacturer vobjManufacturer)
        {
            try
            {
                new ManufacturerDB().Update(ref vobjManufacturer);
            }
            catch (Exception ex)
            {
                
                //TODO: Handle exception;
            }
        }

        public void Delete(ref Manufacturer vobjManufacturer)
        {
            try
            {
                new ManufacturerDB().Delete(ref vobjManufacturer);
            }
            catch (Exception ex)
            {

                //TODO: Handle exception;
            }
        }

        public void ChangeManufacturerStatus(bool vblnIsActive, int vintManufacturerID)
        {
            try
            {
                new ManufacturerDB().ChangeManufacturerStatus(vblnIsActive, vintManufacturerID);
            }
            catch (Exception ex)
            {

                //TODO: Handle exception;
            }

        }

        public List<Manufacturer> GetAll()
        {
            try
            {
                return new ManufacturerDB().GetAll();
            }
            catch (Exception ex)
            {

                //TODO: Handle exception;
                return new List<Manufacturer>();
            }
        }

        public void GetByID(ref Manufacturer vobjManufacturer)
        {
            try
            {
                new ManufacturerDB().GetByID(ref vobjManufacturer);
            }
            catch (Exception ex)
            {

                //TODO: Handle exception;
            }
        }
    }
}
