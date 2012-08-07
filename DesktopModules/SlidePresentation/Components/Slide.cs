using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Content;

namespace Christoc.Com.Modules.SlidePresentation.Components
{
    public class Slide : ContentItem
    {
        //TODO: add sort order
        //TODO: add positioning


        ///<summary>
        /// SlideId is the identifier for our Slides, it matches up to the ID column in the database
        ///</summary>
        public int SlideId { get; set; }
        ///<summary>
        /// Slide Body
        ///</summary>
        public string Body { get; set; }
        ///<summary>
        /// An integer for the user id of the user who created the Slide
        ///</summary>
        public int CreatedByUserId { get; set; }
        ///<summary>
        /// An integer for the user id of the user who last updated the Slide
        ///</summary>
        public int LastModifiedByUserId { get; set; }
        ///<summary>
        /// The date the Slide was created
        ///</summary>
        public new DateTime CreatedOnDate { get; set; }
        ///<summary>
        /// The date the Slide was updated
        ///</summary>
        public new DateTime LastModifiedOnDate { get; set; }
        ///<summary>
        /// The ModuleId of where the Slide was created and gets displayed
        ///</summary>
        public int ModuleId { get; set; }
        ///<summary>
        /// The portal where the Slide resides
        ///</summary>
        public int PortalId { get; set; }

        //Read Only Props
        ///<summary>
        /// The username of the user who created the Slide
        ///</summary>
        public string CreatedByUser
        {
            get
            {
                return CreatedByUserId != 0 ? DotNetNuke.Entities.Users.UserController.GetUserById(PortalId, CreatedByUserId).Username : Null.NullString;
            }
        }

        ///<summary>
        /// The username of the user who last updated the Slide
        ///</summary>
        public string LastUpdatedByUser
        {
            get
            {
                return LastModifiedByUserId != 0 ? DotNetNuke.Entities.Users.UserController.GetUserById(PortalId, LastModifiedByUserId).Username : Null.NullString;
            }
        }

        ///<summary>
        /// Save the Slide
        ///</summary>
        ///<param name="tabId"></param>
        public void Save(int tabId)
        {
            if (ContentItemId < 0)
            {
                var c = new Content();

                c.CreateContentItem(this, tabId);
            }
            else
            {
                var c = new Content();
                c.UpdateContentItem(this, tabId);
            }
        }

        public static List<Slide> GetSlides(int tabId, int moduleId)
        {
            var c = new Content();
            var listOfSlides = new List<Slide>();
            foreach (var nc in c.GetContentItems(tabId, moduleId))
            {
                var p = ConvertToSlide(nc);
                listOfSlides.Add(p);
            }
            return listOfSlides;
        }

        private static Slide ConvertToSlide(ContentItem ci)
        {
            var p = new Slide
            {
                Body = ci.Content,
                Content = ci.Content,
                ContentItemId = ci.ContentItemId,
                ContentKey = ci.ContentKey,
                ContentTypeId = ci.ContentTypeId,
                CreatedByUserId = ci.CreatedByUserID,
                CreatedOnDate = ci.CreatedOnDate,
                LastModifiedByUserId = ci.LastModifiedByUserID,
                LastModifiedOnDate = ci.LastModifiedOnDate,
                ModuleId = ci.ModuleID,
                TabID = ci.TabID,
                SlideId = ci.ContentItemId
            };

            return p;
        }

        public static Slide SlideLoad(int contentItemid)
        {
            var c = new Content();
            return ConvertToSlide(c.GetContentItem(contentItemid));

        }

        #region IHydratable Implementation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {
            //Call the base classes fill method to populate base class properties
            base.FillInternal(dr);

            SlideId = Null.SetNullInteger(dr["Id"]);
            ModuleId = Null.SetNullInteger(dr["ModuleId"]);
            Body = Null.SetNullString(dr["Body"]);
            //PortalId = Null.SetNullInteger(dr["PortalId"]);
            CreatedByUserId = Null.SetNullInteger(dr["CreatedByUserId"]);
            LastModifiedByUserId = Null.SetNullInteger(dr["LastModifiedByUserId"]);
            CreatedOnDate = Null.SetNullDateTime(dr["CreatedOnDate"]);
            LastModifiedOnDate = Null.SetNullDateTime(dr["LastModifiedOnDate"]);

        }

        /// <summary>
        /// Gets and sets the Key ID
        /// </summary>
        /// <returns>An Integer</returns>
        public override int KeyID
        {
            get { return SlideId; }
            set { SlideId = value; }
        }

        #endregion

    }
}