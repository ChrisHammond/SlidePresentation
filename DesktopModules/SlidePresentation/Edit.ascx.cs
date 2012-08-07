/*
' Copyright (c) 2012 christoc.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using Christoc.Com.Modules.SlidePresentation.Components;
using DotNetNuke.Services.Exceptions;

namespace Christoc.Com.Modules.SlidePresentation
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The EditSlidePresentation class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from SlidePresentationModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit : SlidePresentationModuleBase
    {

        #region Event Handlers

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                //Implement your edit logic for your module

                //check for existing item
                var ci = Request.QueryString["ci"];
                if (ci != null && !Page.IsPostBack)
                {
                    var slide = Slide.SlideLoad(Convert.ToInt32(ci));
                    txtSlideBody.Text = slide.Body;
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion

        protected void lbSubmit_Click(object sender, EventArgs e)
        {

            var ci = Request.QueryString["ci"];

            //save the slide
            var newSlide = new Slide();
            newSlide.Body = txtSlideBody.Text;
            newSlide.CreatedByUserId = newSlide.LastModifiedByUserId = UserInfo.UserID;
            newSlide.CreatedOnDate = DateTime.Now;
            newSlide.ModuleId = ModuleId;

            if (ci != null)
            {
                newSlide.ContentItemId = Convert.ToInt32(ci);
            }

            newSlide.Save(TabId);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());

        }

    }

}