// Function to fetch courses based on selected department
document.getElementById('Departments').addEventListener('change', function () {
    var departmentId = this.value;
    if (departmentId) {
        fetch('/Admin/AdminCourse/GetCourses?departmentId=' + departmentId)
            .then(response => response.json())
            .then(data => {
                var coursesDropdown = document.getElementById('Courses');
                coursesDropdown.innerHTML = ''; // Clear previous options
                data.forEach(course => {
                    var option = document.createElement('option');
                    option.value = course.value;
                    option.text = course.text;
                    coursesDropdown.appendChild(option);
                });
            });
    }
});