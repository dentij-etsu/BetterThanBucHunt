/* 
 * Ping-Pong Fever!
 * 
 * Base game created by straker on GitHub
 *  https://gist.github.com/straker/81b59eecf70da93af396f963596dfdc5
 * 
 * Extended by Chris Seals and Jacob Klucher
 * 
 * Fall 2023, ETSU
 */

const canvas = document.getElementById('game');
const context = canvas.getContext('2d');
const grid = 15; // Standard size used by most elements. Also provides offset to prevent elements from going off the left side.
const paddleWidth = grid * 5; // 75 normally
const maxPaddleX = canvas.width - grid - paddleWidth; // The furthest that a paddle can move to the right

var paddleSpeed = 6; // Speed that the paddle moves per tick
var ballSpeed = 6; // Speed that the ball moves per tick
var playerScore = 0;
var computerScore = 0;
var resetting = false;

// Struct which holds the data for the top paddle (the computer)
const topPaddle = {
    // start in the middle of the game on the top side
    // X and y position of this object is the top left point
    y: grid * 2,
    x: canvas.width / 2 - paddleWidth / 2,
    height: grid,
    width: paddleWidth,

    // paddle velocity
    dy: 0
};

// Struct which holds data for the bottom paddle (the user)
const bottomPaddle = {
    // start in the middle of the game on the bottom side
    // X and y position of this object is the top left point
    y: canvas.height - grid * 3,
    x: canvas.width / 2 - paddleWidth / 2,
    height: grid,
    width: paddleWidth,

    // paddle velocity
    dy: 0
};

// Struct which holds the data for the ball
const ball = {
    // start in the middle of the game
    // X and y position of the ball is the top left corner
    x: canvas.width / 2 - grid / 2, // Adjust for grid size
    y: canvas.height / 2 - grid / 2, // Adjust for grid size
    width: grid,
    height: grid,

    // keep track of when need to reset the ball position
    resetting: false,

    // ball velocity (start going to the top-right corner)
    dy: ballSpeed,
    dx: -ballSpeed
};

// check for collision between two objects using axis-aligned bounding box (AABB)
// @see https://developer.mozilla.org/en-US/docs/Games/Techniques/2D_collision_detection
function collides(obj1, obj2) {
    return obj1.x < obj2.x + obj2.width &&
        obj1.x + obj1.width > obj2.x &&
        obj1.y < obj2.y + obj2.height &&
        obj1.y + obj1.height > obj2.y;
}

// Add an AI-controlled paddle
const aiPaddleSpeed = 2; // Adjust the AI paddle speed as needed

// Function to control the AI paddle
function controlAIPaddle() {
    // Calculate the AI paddle's target position based on the ball's position
    const targetX = ball.x - topPaddle.width / 2;

    // Calculate the difference between the current position and the target position
    const dx = targetX - topPaddle.x;

    // Limit the AI paddle's maximum speed
    const aiPaddleVelocity = Math.min(aiPaddleSpeed, Math.abs(dx));

    // Move the AI paddle towards the target position
    if (dx > 0) {
        topPaddle.dy = aiPaddleVelocity;
    } else {
        topPaddle.dy = -aiPaddleVelocity;
    }
}

// Function to reset the game
function resetGame() {
    // Reset the ball and paddle positions
    ball.x = canvas.width / 2 - grid / 2; // Adjust for grid size
    ball.y = canvas.height / 2 - grid / 2; // Adjust for grid size
    // Recenter the two paddles
    topPaddle.x = canvas.width / 2 - paddleWidth / 2;
    bottomPaddle.x = canvas.width / 2 - paddleWidth / 2;

    // Reset the ball's velocity
    ball.dy = ballSpeed;
    ball.dx = -ballSpeed;
    resetting = false;
}

// Function to end the game
function endGame() {
    resetting = true;
    // Clear the canvas
    context.clearRect(0, 0, canvas.width, canvas.height);
    // Display the winner
    const winner = playerScore === 7 ? "Player" : "Computer";
    context.font = '36px Arial';
    context.fillText(`${ winner } wins!`, canvas.width / 2 - 100, canvas.height / 2);
    
    // Stop the game loop
    cancelAnimationFrame(loop);
}

// game loop
function loop() {
    
    requestAnimationFrame(loop);
    context.clearRect(0, 0, canvas.width, canvas.height);

    // Control the AI paddle
    controlAIPaddle();

    // move paddles by their velocity
    topPaddle.x += topPaddle.dy;
    bottomPaddle.x += bottomPaddle.dy;

    // prevent paddles from going through walls
    if (topPaddle.x < grid) {
        topPaddle.x = grid;
    }
    else if (topPaddle.x > maxPaddleX) {
        topPaddle.x = maxPaddleX;
    }

    if (bottomPaddle.x < grid) {
        bottomPaddle.x = grid;
    }
    else if (bottomPaddle.x > maxPaddleX) {
        bottomPaddle.x = maxPaddleX;
    }

    // draw paddles
    context.fillStyle = 'black';
    context.fillRect(topPaddle.x, topPaddle.y, topPaddle.width, topPaddle.height);
    context.fillRect(bottomPaddle.x, bottomPaddle.y, bottomPaddle.width, bottomPaddle.height);

    // move ball by its velocity
    ball.x += ball.dx;
    ball.y += ball.dy;

    // prevent ball from going through walls by changing its velocity
    if (ball.x < grid) {
        ball.x = grid;
        ball.dx *= -1;
    }
    else if (ball.x + grid > canvas.width - grid) {
        ball.x = canvas.width - grid * 2;
        ball.dx *= -1;
    }

    // reset ball if computer scores
    if ((ball.y > canvas.height) && !resetting) {
        computerScore++;
        if (computerScore !== 7)
        {
            resetting = true;
            setTimeout(function () {
                resetGame();
            }, 1000);
        }
    }
    // reset ball if player scores
    if (ball.y < 0 && !resetting) {
        playerScore++;
        if (playerScore !== 7) {
            resetting = true;
            setTimeout(function () {
                resetGame();
            }, 1000);
        }
    }

    // Display the scores
    context.font = '24px Arial';
    context.fillText(`Player: ${ playerScore }`, 20, 30);
    context.fillText(`Computer: ${ computerScore }`, canvas.width - 200, 30);

    // End the game if either player or computer reaches 7 points
    if (playerScore === 7 || computerScore === 7) {
        endGame();
    }

    // check to see if ball collides with paddle. if they do change y velocity
    if (collides(ball, topPaddle)) {
        ball.dy *= -1;

        // move ball next to the paddle otherwise the collision will happen again
        // in the next frame
        ball.y = topPaddle.y + topPaddle.height;
    }
    else if (collides(ball, bottomPaddle)) {
        ball.dy *= -1;

        // move ball next to the paddle otherwise the collision will happen again
        // in the next frame
        ball.y = bottomPaddle.y - ball.height;
    }

    // draw ball
    context.fillRect(ball.x, ball.y, ball.width, ball.height);

    // draw walls
    context.fillStyle = 'black';
    context.fillRect(0, 0, grid, canvas.height);
    context.fillRect(canvas.width - grid, 0, canvas.width, canvas.height);

    
}

// listen to keyboard events to move the paddles
document.addEventListener('keydown', function (e) {

    //left arrow key
    if (e.which === 37) {
        bottomPaddle.dy = -paddleSpeed;
    }

    //right arrow key
    else if (e.which === 39) {
        bottomPaddle.dy = paddleSpeed;
    }
});

// listen to keyboard events to stop the paddle if key is released
document.addEventListener('keyup', function (e) {
    if (e.which === 37 || e.which === 39) {
        bottomPaddle.dy = 0;
    }
});

// start the game
requestAnimationFrame(loop);