document.addEventListener('DOMContentLoaded', function () {
    // Variables pour le slider
    const gallery = document.querySelector('.gallery_main-block');
    const prevBtn = document.querySelector('.prev-btn_main-block');
    const nextBtn = document.querySelector('.next-btn_main-block');
    const images = gallery.querySelectorAll('img');
    let currentIndex = 0;
    const imageWidth = 100;

    // Initialiser le slider
    function updateSlider() {
        gallery.style.transform = `translateX(-${currentIndex * imageWidth}%)`;
    }

    // Événements boutons du slider
    if (prevBtn) {
        prevBtn.addEventListener('click', function () {
            currentIndex = (currentIndex > 0) ? currentIndex - 1 : images.length - 1;
            updateSlider();
        });
    }

    if (nextBtn) {
        nextBtn.addEventListener('click', function () {
            currentIndex = (currentIndex < images.length - 1) ? currentIndex + 1 : 0;
            updateSlider();
        });
    }

    // Auto-rotation du slider
    setInterval(function () {
        currentIndex = (currentIndex < images.length - 1) ? currentIndex + 1 : 0;
        updateSlider();
    }, 5000);

    // Menu mobile toggle
    const menuToggle = document.querySelector('.menu-toggle');
    const menuNavMobile = document.querySelector('.menu-nav-mobile');

    if (menuToggle && menuNavMobile) {
        menuToggle.addEventListener('click', function () {
            menuNavMobile.classList.toggle('show');
        });
    }

    // Modal de remerciement
    const contactForm = document.getElementById('contactForm');
    const modal = document.getElementById('thank-you-modal');
    const closeBtn = document.querySelector('.close-btn');
    const returnBtn = document.querySelector('.return-btn');

    if (contactForm) {
        contactForm.addEventListener('submit', function (event) {
            event.preventDefault();

            // Afficher le modal
            if (modal) modal.style.display = 'block';

            // Réinitialiser le formulaire
            contactForm.reset();
        });
    }

    if (closeBtn && modal) {
        closeBtn.addEventListener('click', function () {
            modal.style.display = 'none';
        });
    }

    if (returnBtn && modal) {
        returnBtn.addEventListener('click', function () {
            modal.style.display = 'none';
        });
    }

    // Fermer le modal
    window.addEventListener('click', function (event) {
        if (modal && event.target === modal) {
            modal.style.display = 'none';
        }
    });
});