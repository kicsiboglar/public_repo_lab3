let img_element = document.getElementById("bouncing-image");
let x_coord = 20;
let y_coord = 20;
let x_speed = 5;
let y_speed = 5;

function run_animation_loop() {
  var window_w = window.innerWidth;
  var window_h = window.innerHeight;

  x_coord = x_coord + x_speed;
  y_coord = y_coord + y_speed;

  if (x_coord + 80 >= window_w || x_coord <= 0) {
    x_speed = x_speed * -1;
    var r_h = Math.floor(Math.random() * 360);
    img_element.style.filter = "hue-rotate(" + r_h + "deg)";
  }

  if (y_coord + 80 >= window_h || y_coord <= 0) {
    y_speed = y_speed * -1;
    var r_h2 = Math.floor(Math.random() * 360);
    img_element.style.filter = "hue-rotate(" + r_h2 + "deg)";
  }

  img_element.style.left = x_coord + "px";
  img_element.style.top = y_coord + "px";

  requestAnimationFrame(run_animation_loop);
}

run_animation_loop();
