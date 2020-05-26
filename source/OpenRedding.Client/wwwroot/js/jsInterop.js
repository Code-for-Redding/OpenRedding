// JavaScript interop code to interface with the application
window.initParticlesJs = () => {
    let config = {
        // normal options
        selector: '.particle-background',
        maxParticles: 450,
        // options for breakpoints
        responsive: [
            {
                breakpoint: 768,
                options: {
                    maxParticles: 200,
                    color: '#48F2E3',
                    connectParticles: false
                }
            }, {
                breakpoint: 425,
                options: {
                    maxParticles: 100,
                    connectParticles: true
                }
            }, {
                breakpoint: 320,
                options: {
                    maxParticles: 0
                    // disables particles.js
                }
            }
        ]
    };

    Particles.init(config);
};

window.interactWithModal = (elementId, status) => {
    $(elementId).modal(status)
};

window.openLoadingModal = () => {
    $('loading-modal').modal('show');
};

window.closeLoadingModal = () => {
    $('loading-modal').modal('hide');
}