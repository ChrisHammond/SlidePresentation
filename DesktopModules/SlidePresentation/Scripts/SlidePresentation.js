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

    var DEFAULT_SETTINGS = {
        //No settings currently defined
        //sortOrder:0
    };

    var DEFAULT_CLASSES = {
        slideText:"slide-input-box",
        slideSaveButton:"dnnPrimaryFormAction"
    };

    //get slides on initialization
    this.init = function(element) {

        var sf = $.ServicesFramework(moduleId);
        var data = {};// serviceFramework.getAntiForgeryProperty(moduleId);
        sf.getAntiForgeryProperty();
        data.moduleId = moduleId;
        data.tabId = tabId;
        serviceFramework.getAntiForgeryProperty();
        $.ajax({
            type: "POST",
            cache: false,
            url: baseServicePath + 'GetSlides',
            data: data
        }).done(function(data) {
            viewModel.slides = ko.utils.arrayMap(data, function(slide) {
                return new Slide(slide);
            });
            ko.applyBindings(viewModel);

            $('#SlidePresentation').jmpress();
            
        }).fail(function () {
            Console.Log('Sorry failed to load Slides');
        });
    };

    //TODO: build out add slide JS
        
    function AddSlide(slide) {
            
    }
        

    //TODO: build out the delete slide JS
        
    //TODO: build out the edit slide JS
        
}