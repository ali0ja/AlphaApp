
const dropdowns = document.querySelectorAll('[data-type="dropdown"]')

document.addEventListener('click', function (event) {
    let clickedDropdown = null

    dropdowns.forEach(dropdown => {
        const targetId = dropdown.getAttribute('data-target')
        const targetElement = document.querySelector(targetId)

        if (dropdown.contains(event.target)) {
            clickedDropdown = targetElement

            document.querySelectorAll('.dropdown.dropdown-show').forEach(openDropdown => {
                if (openDropdown !== targetElement) {
                    openDropdown.classList.remove('dropdown-show')
                }
            })

            targetElement.classList.toggle('dropdown-show')
        }
    })

    if (!clickedDropdown && !event.target.closest('.dropdown')) {
        document.querySelectorAll('.dropdown.dropdown-show').forEach(openDropdown => {
            openDropdown.classList.remove('dropdown-show')
        })
    }
})

document.addEventListener('DOMContentLoaded', () => {
    const previewSize = 150;

    // Öppna modal
    document.querySelectorAll('[data-modal="true"]').forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target');
            const modal = document.querySelector(modalTarget);
            if (modal) modal.style.display = 'flex';
        });
    });

    // Stäng modal
    document.querySelectorAll('[data-close="true"]').forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.mod');
            if (modal) {
                modal.style.display = 'none';
                modal.querySelectorAll('form').forEach(form => {
                    form.reset();

                    const imagePreview = form.querySelector('.image-preview');
                    if (imagePreview) imagePreview.src = '';

                    const imagePreviwer = form.querySelector('.image-previewer');
                    if (imagePreviwer) imagePreviwer.classList.remove('selected');
                });
            }
        });
    });

    // Bildförhandsvisning
    document.querySelectorAll('.image-previewer').forEach(previewer => {
        const fileInput = previewer.querySelector('input[type="file"]');
        const imagePreview = previewer.querySelector('.image-preview');

        previewer.addEventListener('click', () => fileInput.click());
        fileInput.addEventListener('change', ({ target: { files } }) => {
            const file = files[0];
            if (file) processImage(file, imagePreview, previewer, previewSize);
        });
    });

    // Hantera submit endast för modala formulär
    const modals = document.querySelectorAll('.mod');
    modals.forEach(modal => {
        const form = modal.querySelector('form');
        if (form) {
            form.addEventListener('submit', async (e) => {
                e.preventDefault();

                clearErrorMessages(form);
                const formData = new FormData(form);

                try {
                    const res = await fetch(form.action, {
                        method: "post",
                        body: formData
                    });

                    if (res.ok) {
                        modal.style.display = 'none';
                        window.location.reload();
                    } else if (res.status === 400) {
                        const data = await res.json();
                        if (data.errors) {
                            Object.keys(data.errors).forEach(key => {
                                const input = form.querySelector(`[name="${key}"]`);
                                if (input) input.classList.add('input-validation-error');

                                const span = form.querySelector(`[data-valmsg-for="${key}"]`);
                                if (span) {
                                    span.innerText = data.errors[key].join('\n');
                                    span.classList.add('field-validation-error');
                                }
                            });
                        }
                    } else {
                        console.error('Unexpected response:', res.status);
                        alert('Något gick fel. Försök igen senare.');
                    }
                } catch (error) {
                    console.log('Error submitting the form:', error);
                    alert('Ett fel uppstod vid inlämning.');
                }
            });
        }
    });

    // Funktion: rensa fel
    function clearErrorMessages(form) {
        form.querySelectorAll('.input-validation-error').forEach(input => {
            input.classList.remove('input-validation-error');
        });
        form.querySelectorAll('.field-validation-error').forEach(span => {
            span.innerText = '';
            span.classList.remove('field-validation-error');
        });
    }

    // Funktion: ladda bild
    async function loadImage(file) {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.onerror = () => reject(new Error("Misslyckades att läsa fil."));
            reader.onload = (e) => {
                const img = new Image();
                img.onerror = () => reject(new Error("Misslyckades att ladda bild."));
                img.onload = () => resolve(img);
                img.src = e.target.result;
            };
            reader.readAsDataURL(file);
        });
    }

    // Funktion: processa bild
    async function processImage(file, imagePreview, previewer, previewSize = 150) {
        try {
            const img = await loadImage(file);
            const canvas = document.createElement('canvas');
            canvas.width = previewSize;
            canvas.height = previewSize;

            const ctx = canvas.getContext('2d');
            ctx.drawImage(img, 0, 0, previewSize, previewSize);
            imagePreview.src = canvas.toDataURL('image/jpeg');
            previewer.classList.add('selected');
        } catch (error) {
            console.error('Fel vid bildbearbetning:', error);
        }
    }
});


/*Dark mod*/

document.addEventListener("DOMContentLoaded", function () {
    const toggle = document.getElementById('darkModeToggle');
const body = document.body;

// Kolla om dark mode redan är valt
if (localStorage.getItem('theme') === 'dark') {
    body.setAttribute('data-theme', 'dark');
if (toggle) toggle.checked = true;
    }

// Lägg till eventlyssnare
if (toggle) {
    toggle.addEventListener('change', function () {
        if (this.checked) {
            body.setAttribute('data-theme', 'dark');
            localStorage.setItem('theme', 'dark');
        } else {
            body.removeAttribute('data-theme');
            localStorage.setItem('theme', 'light');
        }
    });
    }
});



/*drop*/


document.addEventListener("DOMContentLoaded", function () {
    // Toggle dropdown
    document.querySelectorAll('.card-menu').forEach(menuBtn => {
        menuBtn.addEventListener('click', function (e) {
            e.stopPropagation();
            // Close others
            document.querySelectorAll('.dropdown-menu').forEach(menu => {
                if (menu !== this.nextElementSibling) {
                    menu.style.display = 'none';
                }
            });

            const menu = this.nextElementSibling;
            menu.style.display = menu.style.display === 'block' ? 'none' : 'block';
        });
    });

    // Close on click outside
    document.addEventListener('click', () => {
    document.querySelectorAll('.dropdown-menu').forEach(menu => {
        menu.style.display = 'none';
    });
    });

    // Delete project card
    document.querySelectorAll('.btn-delete').forEach(btn => {
    btn.addEventListener('click', function () {
        const card = this.closest('.project-card');
        card.remove();
    });
    });

    // Add member (placeholder functionality)
    document.querySelectorAll('.btn-add-member').forEach(btn => {
    btn.addEventListener('click', function () {
        const footer = this.closest('.project-card').querySelector('.avatars');
        const newAvatar = document.createElement('img');
        newAvatar.src = 'avatar-placeholder.png'; // Replace with actual image
        newAvatar.alt = 'New Member';
        newAvatar.className = 'avatar';
        footer.appendChild(newAvatar);
    });
    });
});



//fffffffffffffff



    document.addEventListener('DOMContentLoaded', function () {
        const form = document.getElementById('addProjectForm');
    const cardsContainer = document.querySelector('.cards');
    const modal = document.getElementById('editProjectModal');

    form.addEventListener('submit', function (e) {
        e.preventDefault(); // Stoppa form från att skicka

    // Hämta data från formuläret
    const projectName = form.querySelector('input[name="ProjectName"]').value;
    const clientName = form.querySelector('input[name="ClientName"]').value;
    const description = form.querySelector('input[name="Description"]').value;
    const memberName = form.querySelector('input[name="MemberName"]').value;
    const budget = form.querySelector('input[name="Budget"]').value;
    const endDate = form.querySelector('input[name="EndDate"]').value;

    // Skapa nytt kort som HTML
    const newCard = document.createElement('div');
    newCard.className = 'project-card';
    newCard.setAttribute('data-project-name', projectName);
    newCard.setAttribute('data-client-name', clientName);
    newCard.setAttribute('data-description', description);
    newCard.setAttribute('data-member-name', memberName);
    newCard.setAttribute('data-budget', budget);
    newCard.setAttribute('data-end-date', endDate);
    newCard.innerHTML = `
    <div class="card-header">
        <img class="project-logo" src="/images/image.svg" alt="Logo" />
        <div class="card-title">
            <h3>${projectName}</h3>
            <p class="company">${clientName}</p>
        </div>
        <div class="dropdown-wrapper">
            <button class="card-menu"><i class="fa-solid fa-ellipsis"></i></button>
            <ul class="dropdown-menu">
                <li><button class="btn-edit">Edit</button></li>
                <li><button class="btn-add-member">Add Member</button></li>
                <li><button class="btn-delete danger">Delete</button></li>
            </ul>
        </div>
    </div>
    <p class="description">${description}</p>
    <div class="card-footer">
        <span class="badge time-left">
            <i class="fa-regular fa-clock"></i> Ends: ${endDate}
        </span>
        <div class="avatars">
            <img class="avatar" src="/images/user_img.svg" alt="User 1">
                <img class="avatar" src="/images/user_img.svg" alt="User 2">
                </div>
        </div>
        `;

        // Lägg till i DOM
        cardsContainer.appendChild(newCard);

        // Stäng modalen
        modal.classList.remove('open');

        // Återställ formuläret
        form.reset();

        // Återkoppla delete/edit/add-member events
        attachCardEvents(newCard);
        });

        // Återanvändbar funktion för delete/add/edit
        function attachCardEvents(card) {
            card.querySelector('.btn-delete')?.addEventListener('click', () => card.remove());

            card.querySelector('.btn-add-member')?.addEventListener('click', () => {
                const avatars = card.querySelector('.avatars');
        const newAvatar = document.createElement('img');
        newAvatar.src = '/images/user_img.svg';
        newAvatar.className = 'avatar';
        newAvatar.alt = 'New Member';
        avatars.appendChild(newAvatar);
            });

            card.querySelector('.btn-edit')?.addEventListener('click', () => {
                // Redigeringslogik du redan har (se föregående svar)
                const modal = document.getElementById('editProjectModal');
        modal.querySelector('input[name="ProjectName"]').value = card.dataset.projectName;
        modal.querySelector('input[name="ClientName"]').value = card.dataset.clientName;
        modal.querySelector('input[name="Description"]').value = card.dataset.description;
        modal.querySelector('input[name="MemberName"]').value = card.dataset.memberName;
        modal.querySelector('input[name="Budget"]').value = card.dataset.budget;
        modal.querySelector('input[name="EndDate"]').value = card.dataset.endDate;
        modal.classList.add('open');
            });

        card.querySelector('.card-menu')?.addEventListener('click', function (e) {
            e.stopPropagation();
        const dropdown = this.nextElementSibling;
                document.querySelectorAll('.dropdown-menu').forEach(menu => {
                    if (menu !== dropdown) menu.style.display = 'none';
                });
        dropdown.style.display = dropdown.style.display === 'block' ? 'none' : 'block';
            });
        }
    });

