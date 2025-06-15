document.addEventListener('DOMContentLoaded', function () {
    const sectionIds = ['dashboard', 'books', 'members', 'accounting', 'clearance'];

    sectionIds.forEach(sectionId => {
        const link = document.getElementById(sectionId + 'Link');
        if (link) {
            link.addEventListener('click', function (e) {
                e.preventDefault();
                showSection(sectionId);
            });
        }
    });

    // Optional: show default section on load
    showSection('dashboard');
});

function showSection(sectionId) {
    const sectionIds = ['dashboard', 'books', 'members', 'accounting', 'clearance'];

    sectionIds.forEach(id => {
        const section = document.getElementById(id);
        const link = document.getElementById(id + 'Link');

        if (section) {
            section.style.display = (id === sectionId) ? 'block' : 'none';
        }

        if (link) {
            link.classList.toggle('active', id === sectionId);
        }
    });
}
