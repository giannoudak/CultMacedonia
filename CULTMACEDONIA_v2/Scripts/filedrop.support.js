/// <reference path="_references.js" />

$(function () {
    
    var dropbox = $('#dropbox');                                        // get the Dropbox reference
    var message = $('.message', dropbox);                               // get the message reference inside dropbox container

    dropbox.filedrop({
        url: '/Points/UploadFiles',
        paramname: 'files',
        allowedfiletypes: ['image/jpeg', 'image/png', 'image/gif'],     // filetypes allowed by Content-Type.  Empty array means no restrictions
        allowedfileextensions: ['.jpg', '.jpeg', '.png', '.gif'],       // file extensions allowed. Empty array means no restrictions
        maxFiles: 5,                                                    // max files
        maxFileSize: 4,                                                 // max file size in MBs
        
        dragOver: function () {
            // user dragging files over #dropzone
            dropbox.css({ border: '2px dashed gray', background: 'lightblue', color: 'black' });
        },
        dragLeave: function () {
            // user dragging files out of #dropzone
            dropbox.css({ border: '2px solid black', background: 'gray', color: 'white' });
        },
        drop: function(){
            // user drops a file
            dropbox.css('background', 'gray');
        },
        uploadStarted:function(i, file,len){
            // file began uploading
            // i is the index 
            // file is the file
            // len = #files
            

            createImage(file);
        },
        beforeEach: function (file) {
            // file is a file object
            // return false to cancel upload

            if (!file.type.match(/^image\//)) {
                alert('Only images are allowed!');
                
                // Returning false will cause the
                // file to be rejected
                return false;
            }
            
        },
        uploadFinished: function (i, file, response, time) {
            // response is the data you got back from server in JSON format.
            //alert(file.name);
            $.data(file).addClass('done');
        },
        progressUpdated: function (i, file, progress) {
            
            // this function is used for large files and updates intermittently
            // progress is the integer value of file being uploaded percentage to completion
            $.data(file).find('.progress').width(progress);
        },
        globalProgressUpdated: function (progress) {
            // progress for all the files uploaded on the current instance (percentage)
            // ex: $('#progress div').width(progress+"%");
        },

        error: function (err, file) {
            switch (err) {
                case 'BrowserNotSupported':
                    showMessage('Your browser does not support HTML5 file uploads!');
                    break;
                case 'TooManyFiles':
                    alert('Too many files! Please select 10 at most! (configurable)');
                    break;
                case 'FileTooLarge':
                    alert(file.name + ' is too large! Please upload files up to 4mb (configurable).');
                    break;
                case 'FileTypeNotAllowed':
                    alert(file.name + ' is not supported. You can only upload files with .gif .png and .jpg extension');
                    break;
                default:
                    break;
            }
        },
    });


    

    //http://softwarelounge.co.uk/archives/1661
    


    



    var template = '<div class="preview">' +
            '<span class="imageHolder">' +
                '<img />' +
                '<span class="uploaded"></span>' +
            '</span>' +
            '<div class="progressHolder">' +
                '<div class="progress"></div>' +
            '</div>' +
                      '<div class="remove"><a class="delete">remove</a></div>' +
             '</div>';

    function createImage(file) {
        var preview = $(template), image = $('img', preview);
        //var deleteimg = $('.remove', preview);

        var reader = new FileReader();

        image.width = 100;
        image.height = 100;

        reader.onload = function (e) {

            // e.target.result holds the DataURL which
            // can be used as a source of the image:
            
            image.attr('src', e.target.result);
            // var str = "<a class='delete' data-file="+file.name + ">remove</a>";
            $('.preview').on('click', '.delete', function () {
                //$(this).find('#listing-id').toggle("blind", 600);
                alert('sssss')
            });
            //deleteimg.html(str);
        };

        // Reading the file as a DataURL. When finished,
        // this will trigger the onload function above:
        reader.readAsDataURL(file);

        message.hide();
        preview.appendTo(dropbox);

        // Associating a preview container
        // with the file, using jQuery's $.data():

        $.data(file, preview);
    }

    function showMessage(msg) {
        message.html(msg);
    }

});