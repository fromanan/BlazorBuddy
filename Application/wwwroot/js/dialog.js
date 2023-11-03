export function openDialog(id) {
    const htmlElement = document.getElementById(id);
    if (htmlElement instanceof HTMLDialogElement) {
        htmlElement.showModal();
    }
}

export function closeDialog(id) {
    const htmlElement = document.getElementById(id);
    if (htmlElement instanceof HTMLDialogElement) {
        htmlElement.close();
    }
}