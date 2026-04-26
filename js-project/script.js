const fortunes = [
  "A kódod elsőre lefordul, és még te sem érted, hogyan!",
  "A témavezetőd pont ma itta meg a kávéját, várható egy válasz!",
  "A 'Final_DONE_REAL.zip' tényleg az utolsó lesz, ígérem!",
  "A védésen pont azt a kérdést kapod, amire a legjobban készültél.",
  "A kódodban több a 'TODO' megjegyzés, mint a tényleges logika, de működik.",
  "Végre rájöttél, mi volt a hiba: egy pontosvessző. Igen, az a 4 óra megérte.",
];

const cookieBtn = document.getElementById("cookie-btn");
const cookieDisplay = document.getElementById("cookie-display");
const messageDisplay = document.getElementById("message");

cookieBtn.addEventListener("click", () => {
  cookieDisplay.classList.add("shake");
  cookieBtn.disabled = true;

  setTimeout(() => {
    const randomIndex = Math.floor(Math.random() * fortunes.length);
    messageDisplay.innerText = fortunes[randomIndex];
    cookieDisplay.innerText = "💥"; // Felrobbant
    cookieDisplay.classList.remove("shake");
    cookieBtn.innerText = "Kérek még egy löketet!";
    cookieBtn.disabled = false;
  }, 600);
});

const canvas = document.getElementById("particle-canvas");
const ctx = canvas.getContext("2d");

function resizeCanvas() {
  canvas.width = window.innerWidth;
  canvas.height = window.innerHeight;
}
window.addEventListener("resize", resizeCanvas);
resizeCanvas();

let mouse = { x: null, y: null };
window.addEventListener("mousemove", (event) => {
  mouse.x = event.x;
  mouse.y = event.y;
});

class Particle {
  constructor() {
    this.x = Math.random() * canvas.width;
    this.y = Math.random() * canvas.height;
    this.size = Math.random() * 3 + 1;
    this.speedX = Math.random() * 1 - 0.5;
    this.speedY = Math.random() * 1 - 0.5;
    this.color = "rgba(231, 76, 60, 0.5)";
  }

  update() {
    this.x += this.speedX;
    this.y += this.speedY;

    if (this.x > canvas.width || this.x < 0) this.speedX *= -1;
    if (this.y > canvas.height || this.y < 0) this.speedY *= -1;

    if (mouse.x && mouse.y) {
      let dx = mouse.x - this.x;
      let dy = mouse.y - this.y;
      let distance = Math.sqrt(dx * dx + dy * dy);
      if (distance < 100) {
        this.x -= dx / 10;
        this.y -= dy / 10;
      }
    }
  }

  draw() {
    ctx.fillStyle = this.color;
    ctx.beginPath();
    ctx.arc(this.x, this.y, this.size, 0, Math.PI * 2);
    ctx.fill();
  }
}

const particlesArray = [];
const numberOfParticles = 100;
for (let i = 0; i < numberOfParticles; i++) {
  particlesArray.push(new Particle());
}

function animate() {
  ctx.clearRect(0, 0, canvas.width, canvas.height);

  for (let i = 0; i < particlesArray.length; i++) {
    particlesArray[i].update();
    particlesArray[i].draw();
  }

  requestAnimationFrame(animate);
}

animate();
