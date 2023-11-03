export function debounceEvent(htmlElement, eventName, delay) {
    registerEvent(htmlElement, eventName, delay, debounce);
}

export function throttleEvent(htmlElement, eventName, delay) {
    registerEvent(htmlElement, eventName, delay, throttle);
}

function registerEvent(htmlElement, eventName, delay, filterFunction) {
    let raisingEvent = false;
    let eventHandler = filterFunction(function (e) {
        raisingEvent = true;
        try {
            htmlElement.dispatchEvent(e);
        } finally {
            raisingEvent = false;
        }
    }, delay);

    htmlElement.addEventListener(eventName, e => {
        if (!raisingEvent) {
            e.stopImmediatePropagation();
            eventHandler(e);
        }
    });
}

function debounce(func, wait) {
    let timer;
    return (...args) => {
        clearTimeout(timer);
        timer = setTimeout(() => { func.apply(this, args); }, wait);
    };
}

function throttle(func, wait) {
    let context, args, result;
    let timeout = null;
    let previous = 0;
    const later = function () {
        previous = Date.now();
        timeout = null;
        result = func.apply(context, args);
        if (!timeout) context = args = null;
    };
    return function () {
        const now = Date.now();
        if (!previous) previous = now;
        const remaining = wait - (now - previous);
        context = this;
        args = arguments;

        if (!timeout) {
            timeout = setTimeout(later, remaining);
        }
        return result;
    };
}