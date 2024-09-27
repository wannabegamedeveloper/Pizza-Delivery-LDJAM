mergeInto(LibraryManager.library, 
{

    SendMessageToPlatform: function (data)
    {
        console.info(UTF8ToString(data));
    },
});