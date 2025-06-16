document.addEventListener('DOMContentLoaded', function () {
    const sectionIds = ['dashboard', 'books', 'members', 'accounting', 'clearance'];

    // Section tab handlers
    sectionIds.forEach(sectionId => {
        const link = document.getElementById(`${sectionId}Link`);
        if (link) {
            link.addEventListener('click', function (e) {
                e.preventDefault();
                showSection(sectionId);
            });
        }
    });

    // Show default section
    showSection('dashboard');

    // Make sure appData is available
    if (!window.appData) {
        console.error('window.appData is not defined.');
        return;
    }

    const booksData = window.appData.books || [];
    const membersData = window.appData.members || [];

    // Book Edit Button Handlers
    document.querySelectorAll('[data-bs-target="#editBookModal"]').forEach(button => {
        button.addEventListener('click', function () {
            const bookId = this.getAttribute('data-id');
            const book = booksData.find(b => `${b.Id}` === `${bookId}`);

            if (!book) {
                console.warn(`Book with ID ${bookId} not found.`);
                return;
            }

            const modal = document.getElementById('editBookModal');
            if (!modal) return;

            modal.querySelector('input[name="Id"]').value = book.Id;
            modal.querySelector('input[name="Title"]').value = book.Title || '';
            modal.querySelector('input[name="Author"]').value = book.Author || '';
            modal.querySelector('input[name="ISBN"]').value = book.ISBN || '';
            modal.querySelector('input[name="Category"]').value = book.Category || '';
            modal.querySelector('input[name="TotalCopies"]').value = book.TotalCopies || 1;

            bootstrap.Modal.getOrCreateInstance(modal).show();
        });
    });

    // Member Edit Button Handlers
    document.querySelectorAll('[data-bs-target="#editMemberModal"]').forEach(button => {
        button.addEventListener('click', function () {
            const memberId = this.getAttribute('data-id');
            const member = membersData.find(m => `${m.Id}` === `${memberId}`);

            if (!member) {
                console.warn(`Member with ID ${memberId} not found.`);
                return;
            }

            const modal = document.getElementById('editMemberModal');
            if (!modal) return;

            modal.querySelector('input[name="Id"]').value = member.Id;
            modal.querySelector('input[name="FullName"]').value = member.FullName || '';
            modal.querySelector('input[name="Email"]').value = member.Email || '';
            modal.querySelector('input[name="ContactNumber"]').value = member.ContactNumber || '';
            modal.querySelector('select[name="MembershipType"]').value = member.MembershipType || '';
            modal.querySelector('input[name="MembershipStartDate"]').value = (member.MembershipStartDate || '').substring(0, 10);

            bootstrap.Modal.getOrCreateInstance(modal).show();
        });
    });
});

function showSection(sectionId) {
    const sectionIds = ['dashboard', 'books', 'members', 'accounting', 'clearance'];

    sectionIds.forEach(id => {
        const section = document.getElementById(id);
        const link = document.getElementById(`${id}Link`);

        if (section) {
            section.style.display = (id === sectionId) ? 'block' : 'none';
        }

        if (link) {
            link.classList.toggle('active', id === sectionId);
        }
    });
}


    function filterTable(inputId, tableSelector) {
    const input = document.getElementById(inputId);
    const filter = input.value.toLowerCase();
    const rows = document.querySelectorAll(`${tableSelector} tbody tr`);

    rows.forEach(row => {
    const text = row.innerText.toLowerCase();
    row.style.display = text.includes(filter) ? '' : 'none';
});
}

    document.addEventListener('DOMContentLoaded', function () {
    const bookInput = document.getElementById('bookSearchInput');
    const memberInput = document.getElementById('memberSearchInput');

    if (bookInput) {
    bookInput.addEventListener('input', () => filterTable('bookSearchInput', '#books table'));
}

    if (memberInput) {
    memberInput.addEventListener('input', () => filterTable('memberSearchInput', '#members table'));
}
});


