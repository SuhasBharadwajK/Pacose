import { trigger, transition, style, query, animateChild, animate, group } from "@angular/animations";

export const routeAnimations = trigger('routeAnimations', [
    transition('* => left', slideTo('right')),
    transition('left => *', slideTo('left')),
    transition('* => top', slideTo('top')),
    transition('top => *', slideTo('bottom')),
    transition('* => bottom', slideTo('bottom')),
    transition('bottom => *', slideTo('top')),
]);

function slideTo(direction) {
    const optional = { optional: true };
    const isVertical = direction === 'top' || direction === 'bottom';
    const amount = isVertical ? `calc(100% + {{heightDiff}}px)` : '100%';

    return [
        query(':enter', [
            style({
                position: 'absolute',
                overflow: isVertical ? 'hidden' : 'auto',
                [direction]: '0px',
                [isVertical ? 'height' : 'width']: '100%'
            })
        ], optional),
        query(':leave', [
            style({
                position: 'absolute',
                [direction]: '0px',
                [isVertical ? 'height' : 'width']: amount
            })
        ], optional),
        query(':enter', [
            style({ 
                [direction]: '-100%',
            })
        ], optional),
        group([
            query(':leave', [
                animate(`${ isVertical ? 400 : 380 }ms cubic-bezier(0.0, 0.0, 0.2, 1)`, style({
                    [direction]: '100%'
                }))
            ], optional),
            query(':enter', [
                animate(`${ isVertical ? 400 : 380 }ms cubic-bezier(0.0, 0.0, 0.2, 1)`, style({
                    [direction]: '0%'
                }))
            ], optional),
        ])
    ];
}
