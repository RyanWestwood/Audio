studio.menu.addMenuItem({ name: "Save and Build", execute: function buildAndCopy() {
    studio.project.save();
    studio.project.build();
    alert("Save and Build complete!");
}});