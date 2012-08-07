
function SlidePresentation($, ko, settings) {
    var moduleId = settings.moduleId;
    var tabId = settings.tabId;
    var serviceFramework = settings.servicesFramework;
    var baseServicePath = serviceFramework.getServiceRoot('SlidePresentation') + 'SlidePresentation.ashx/';

    function Slide(s) {
        this.SlideId = s.SlideId;
        this.Body = s.Body;
        this.XVal = s.SlideId * Math.random()*100;
        this.YVal = s.SlideId * Math.random()*100;
    }

    var viewModel = {
        slides: ko.observableArray([])
    };

    //= '/desktopmodules/SlidePresentation/API/SlidePresentation.ashx/GetSlides',
    //= serviceFramework.getServiceRoot('MemberDirectory') + 'MemberDirectory.ashx/';

    var DEFAULT_SETTINGS = {
        //No settings currently defined
        //sortOrder:0
    };

    var DEFAULT_CLASSES = {
        slideText:"slide-input-box",
        slideSaveButton:"dnnPrimaryFormAction"
    };

    //TODO: get slides

    this.init = function(element) {

        var sf = $.ServicesFramework(moduleId);
        var passedData = [];// serviceFramework.getAntiForgeryProperty(moduleId);
        sf.getAntiForgeryProperty();
        passedData.moduleId = moduleId;
        passedData.tabId = tabId;
        serviceFramework.getAntiForgeryProperty();
        $.ajax({
            type: "POST",
            cache: false,
            url: baseServicePath + 'GetSlides',
            data: passedData
        }).done(function(data) {
            viewModel.slides = ko.utils.arrayMap(data, function(slide) {
                return new Slide(slide);
            });
            ko.applyBindings(viewModel);
            $('#simple').jmpress();
            alert('test');
        }).fail(function () {
            alert('fail');
            Console.Log('Sorry failed to load Slides');
        });
    };

    //TODO: add slide
        
    function AddSlide(slide) {
            
    }
        

    //TODO: delete slide
        
    //TODO: edit slide
        

    

    //// delay 
    //function checkModel() {
    //    if (viewModel.slides.length > 0) {
    //        $('#simple').jmpress();
    //    }
    //    else {
    //        checkModel();
    //    }
    //}
    //setTimeout("checkModel()", 500);

}