function loadComments(projectId) {
    $.ajax({
        url: '/ProjectManagement/ProjectComment/GetComments?projectId=' + projectId,
        method: 'GET',
        success: function (data) {
            var commentsHtml = '';
            for (var i = 0; i < data.length; i++) {
                commentsHtml += '<div class="card mb-3">';
                commentsHtml += '<div class="card-body">';
                commentsHtml += '<p class="card-text">' + data[i].content + '</p>';
                commentsHtml += '<span>Posted on: ' + new Date(data[i].createdDate).toLocaleString() + '</span>';
                commentsHtml += '</div>';
                commentsHtml += '</div>';
            }
            $('#commentsList').html(commentsHtml);
        }
    });
};

$(document).ready(function () {
    var projectId = $('#projectComments input[name="projectId"]').val(); // Changed "ProjectId" to "projectId"
    loadComments(projectId);

    $('#addCommentForm').submit(function (e) {
        e.preventDefault();
        var formData = {
            projectId: projectId,
            content: $('#projectComments textarea[name="content"]').val() // Changed "Content" to "content"
        };

        $.ajax({
            url: '/ProjectManagement/ProjectComment/AddComment',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response) {
                if (response.success) {
                    $('#projectComments textarea[name="content"]').val(''); // Changed "Content" to "content"
                    loadComments(projectId);
                } else {
                    alert(response.message);
                }
            },
            error: function (xhr, status, error) {
                alert("Error: " + error);
            }
        });
    });
});
