﻿function SlidePresentation($, ko, settings) {
    var moduleId = settings.moduleId;
    var tabId = settings.tabId;
    var serviceFramework = settings.servicesFramework;
    var baseServicePath = serviceFramework.getServiceRoot('SlidePresentation') + 'SlidePresentation.ashx/';

    function slide(s) {
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
        //var data = {}; //removed because we don't need this
        //data.moduleId = moduleId; //removed because we don't need this when calling setModuleHeaders
        //data.tabId = tabId; //removed because we don't need this
        //serviceFramework.getAntiForgeryProperty(); //removed because we don't need this
        $.ajax({
            type: "POST",
            cache: false,
            url: baseServicePath + 'ListOfSlides',
            //data: data,
            //dataType:"json",
            beforeSend: serviceFramework.setModuleHeaders
        }).done(function(data) {
            viewModel.slides = ko.utils.arrayMap(data, function(s) {
                return new slide(s);
            });
            ko.applyBindings(viewModel);
            $(element).jmpress();
        }).fail(function () {
            Console.Log('Sorry failed to load Slides');
        });
    };

    //TODO: build out add slide JS       
    function AddSlide(slide) {
            
    }

    //todo: show the slide creation/edit interface



    //TODO: build out the delete slide JS
    //TODO: build out the edit slide JS        
}