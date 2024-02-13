/*
 * This file handles the general touch events using Hammer JS.
 */

var game = document.getElementById("game");
if (game != null) { //used so the browser doesn't freak out if not at a game page

    const canvasSwipe = document.getElementById('game');

    canvasSwipe.addEventListener('touchmove', (e) => {
        e.preventDefault(); // Prevent the default touchmove behavior
    });

    const hammer = new Hammer(canvasSwipe);
    hammer.get('swipe').set({ direction: Hammer.DIRECTION_ALL });

    hammer.on('swipe', (e) => {
        const direction = e.direction;

        if (direction === Hammer.DIRECTION_LEFT) {
            // Swipe left action
            simulateArrowKeyPress(37); // 37 is the key code for the left arrow
        } else if (direction === Hammer.DIRECTION_RIGHT) {
            // Swipe right action
            simulateArrowKeyPress(39); // 39 is the key code for the right arrow
        } else if (direction === Hammer.DIRECTION_UP) {
            // Swipe up action
            simulateArrowKeyPress(38); // 38 is the key code for the up arrow
        } else if (direction === Hammer.DIRECTION_DOWN) {
            // Swipe down action
            simulateArrowKeyPress(40); // 40 is the key code for the down arrow
        }
    });

    function simulateArrowKeyPress(keyCode) {
        const event = new KeyboardEvent('keydown', {
            key: '',
            keyCode: keyCode,
            which: keyCode,
        });
        document.dispatchEvent(event);
    }
}