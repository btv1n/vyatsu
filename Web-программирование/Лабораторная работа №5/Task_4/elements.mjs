function createElement(tagName = "div", children = [], attributes = {}, events = {}) {
    const element = document.createElement(tagName);

    if (Array.isArray(children))
        children.forEach(child => element.appendChild(child));
    else if (typeof children === typeof "")
        element.appendChild(document.createTextNode(children));
    else
        element.appendChild(children);

    for (const key in attributes) {
        element.setAttribute(key, attributes[key])
    }

    for (const key in events) {
        element.addEventListener(key, events[key]);
    }

    return element;
}

export function tag(tagName, ...children) {
    let events = {};
    let attributes = {};
    const preparedChildren = [];

    children.forEach((value, index) => {
        if (typeof value === typeof "") {
            preparedChildren.push(
                document.createTextNode(value)
            );
        } else if (value.__modifier) {
            if (value.attributes)
                attributes = { ...attributes, ...value.attributes };
            if (value.events)
                events = { ...events, ...value.events };
        } else if (typeof value === "function") {
            preparedChildren.push(value());
        } else {
            preparedChildren.push(value);
        }
    });

    return createElement(tagName, preparedChildren, attributes, events)
}

export function events(e = {}) {
    return { __modifier: true, events: e };
}

export function attributes(a = {}) {
    return { __modifier: true, attributes: a };
}


export function cls(c = {}) {
    let classList = [];
    if (typeof c === typeof "")
        classList = c.split(" ");
    else if (typeof c === typeof {})
        for (const cls in c) {
            if (c[cls]) classList.push(cls);
        }
    else
        throw new Error("Invalid cls argument type: " === typeof c);

    return attributes({ class: classList.join(" ") });
}

export function style(s = {}) {
    return attributes({ style: Object.entries(s).map(([k, v]) => `${k}:${v}`).join(';') });
}


export function div(...children) {
    return tag("div", ...children);
}

export function ul(...children) {
    return tag("ul", ...children);
}

export function li(...children) {
    return tag("li", ...children);
}

export function input(...children) {
    return tag("input", ...children);
}

export function select(...children) {
    return tag("select", ...children);
}

export function option(...children) {
    return tag("option", ...children);
}

export function slot(component, wrapper = undefined, ...args) {
    if (wrapper === undefined) {
        wrapper = div();
    } else if (typeof wrapper === "function") {
        wrapper = wrapper();
    }
    const target = component(rerender, ...args);

    function rerender() {
        wrapper.innerHTML = '';
        wrapper.appendChild(target())
    }

    rerender();

    wrapper.rerender = rerender;
    return wrapper
}

export function useRerender(wrapper, target) {
    return () => {
        wrapper.innerHTML = '';
        const content = target();
        if (Array.isArray(content))
            content.forEach(e => wrapper.appendChild(e));
        else
            wrapper.appendChild(content);
    };
}

export function mount(targetId, element) {
    const target = document.getElementById(targetId);
    if (!target)
        throw Error("target not found");

    if (typeof element === "function")
        element = element();
    target.appendChild(element);
}