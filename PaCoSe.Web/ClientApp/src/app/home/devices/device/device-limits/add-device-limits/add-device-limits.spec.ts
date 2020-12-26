import { LimitInterval } from "../../../../../common/models/limit-interval";
import { DayInWeek } from "../day-in-week";
import { DeviceLimitsService } from "./device-limits.service";

describe('Indices Test Suite', () => {
    let service = new DeviceLimitsService();
    const init = () => {
        service.init(new DayInWeek({
            dayOfTheWeek: 0,
            dayName: 'Sunday',
            allowedHours: 4,
            intervals: [
                new LimitInterval({
                    startTime: 2,
                    endTime: 3
                }),
                new LimitInterval({
                    startTime: 5,
                    endTime: 6
                }),
                new LimitInterval({
                    startTime: 8,
                    endTime: 9
                }),
                new LimitInterval({
                    startTime: 11,
                    endTime: 12.5
                }),
                new LimitInterval({
                    startTime: 16,
                    endTime: 18
                }),
            ]
        }));
    }

    init();

    //#region TestingIndices

    //-----------------------------------------------------------------------------------------------------------------
    //---------------------------------------------Testing Indices-----------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------

    // [{2, 3}, {5, 6}, {8, 9}, {11, 12.5}, {16, 18}]; -> Initial state


    it('1.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 3,
            endTime: 7
        }));

        expect(headIndex).toBe(0);
        expect(tailIndex).toBe(1);
    });

    it('2.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 5.5,
            endTime: 8.5
        }));

        expect(headIndex).toBe(1);
        expect(tailIndex).toBe(2);
    });

    it('3.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 5.5,
            endTime: 12.5
        }));

        expect(headIndex).toBe(1);
        expect(tailIndex).toBe(3);
    });

    it('4.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 7,
            endTime: 12
        }));

        expect(headIndex).toBe(2);
        expect(tailIndex).toBe(3);
    });

    it('5.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 7,
            endTime: 13
        }));

        expect(headIndex).toBe(2);
        expect(tailIndex).toBe(3);
    });

    it('6.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 8.5,
            endTime: 12.5
        }));

        expect(headIndex).toBe(2);
        expect(tailIndex).toBe(3);
    });

    it('7.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 2.5,
            endTime: 5
        }));

        expect(headIndex).toBe(0);
        expect(tailIndex).toBe(1);
    });

    it('8.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 7,
            endTime: 10
        }));

        expect(headIndex).toBe(2);
        expect(tailIndex).toBe(2);
    });

    it('9.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 5,
            endTime: 12.5
        }));

        expect(headIndex).toBe(1);
        expect(tailIndex).toBe(3);
    });

    it('10.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 5,
            endTime: 10
        }));

        expect(headIndex).toBe(1);
        expect(tailIndex).toBe(2);
    });

    it('11.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 5,
            endTime: 7
        }));

        expect(headIndex).toBe(1);
        expect(tailIndex).toBe(1);
    });

    it('12.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 7,
            endTime: 12.5
        }));

        expect(headIndex).toBe(2);
        expect(tailIndex).toBe(3);
    });

    it('13.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 15,
            endTime: 18
        }));

        expect(headIndex).toBe(4);
        expect(tailIndex).toBe(4);
    });

    it('14.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 5,
            endTime: 8.5
        }));

        expect(headIndex).toBe(1);
        expect(tailIndex).toBe(2);
    });

    it('15.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 5,
            endTime: 8
        }));

        expect(headIndex).toBe(1);
        expect(tailIndex).toBe(2);
    });

    it('16.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 6,
            endTime: 7
        }));

        expect(headIndex).toBe(1);
        expect(tailIndex).toBe(1);
    });

    it('17.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 6,
            endTime: 8.5
        }));

        expect(headIndex).toBe(1);
        expect(tailIndex).toBe(2);
    });

    it('18.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 6,
            endTime: 11
        }));

        expect(headIndex).toBe(1);
        expect(tailIndex).toBe(3);
    });

    it('19.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 1,
            endTime: 2.5
        }));

        expect(headIndex).toBe(0);
        expect(tailIndex).toBe(0);
    });

    it('20.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 2.5,
            endTime: 7
        }));

        expect(headIndex).toBe(0);
        expect(tailIndex).toBe(1);
    });

    it('21.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 1,
            endTime: 8.5
        }));

        expect(headIndex).toBe(0);
        expect(tailIndex).toBe(2);
    });

    it('22.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 1,
            endTime: 2
        }));

        expect(headIndex).toBe(0);
        expect(tailIndex).toBe(0);
    });

    it('23.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 2,
            endTime: 4
        }));

        expect(headIndex).toBe(0);
        expect(tailIndex).toBe(0);
    });

    it('24.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 17,
            endTime: 19
        }));

        expect(headIndex).toBe(4);
        expect(tailIndex).toBe(4);
    });

    it('25.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 12,
            endTime: 17
        }));

        expect(headIndex).toBe(3);
        expect(tailIndex).toBe(4);
    });

    it('26.0', () => {
        const { headIndex, tailIndex } = service.getHeadAndTailIndices(service.dayInWeek.intervals, new LimitInterval({
            startTime: 7,
            endTime: 16
        }));

        expect(headIndex).toBe(2);
        expect(tailIndex).toBe(4);
    });

    //#endregion
});

describe('Insertion Test Suite', () => {

    let service = new DeviceLimitsService();
    const init = () => {
        service.init(new DayInWeek({
            dayOfTheWeek: 0,
            dayName: 'Sunday',
            allowedHours: 4,
            intervals: [
                new LimitInterval({
                    startTime: 2,
                    endTime: 3
                }),
                new LimitInterval({
                    startTime: 5,
                    endTime: 6
                }),
                new LimitInterval({
                    startTime: 8,
                    endTime: 9
                }),
                new LimitInterval({
                    startTime: 11,
                    endTime: 12.5
                }),
                new LimitInterval({
                    startTime: 16,
                    endTime: 18
                }),
            ]
        }));
    }

    init();

    //-----------------------------------------------------------------------------------------------------------------
    //--------------------------------------------Testing Insertions---------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------

    // [{2, 3}, {5, 6}, {8, 9}, {11, 12.5}, {16, 18}]; -> Initial state

    it('1.1: #addInterval before the existing timeline increases the length of the array to 6', () => {
        service.addInterval(new LimitInterval({
            startTime: 0.5,
            endTime: 1.5
        }));
        
        expect(service.dayInWeek.intervals.length).toBe(6);
    });
    
    // [{0.5, 1.5}, {2, 3}, {5, 6}, {8, 9}, {11, 12.5}, {16, 18}]; -> Expected state

    it('1.2: The first element has the expected values after insertion', () => {
        expect(service.dayInWeek.intervals[0].startTime).toBe(0.5);
        expect(service.dayInWeek.intervals[0].endTime).toBe(1.5);
    });

    //-----------------------------------------------------------------------------------------------------------------

    // [{0.5, 1.5}, {2, 3}, {5, 6}, {8, 9}, {11, 12.5}, {16, 18}]; -> Initial state

    it('2.1: #addInterval between {0.5, 1.5} and {2, 3} decreases the length of the array to 5', () => {
        service.addInterval(new LimitInterval({
            startTime: 1,
            endTime: 2
        }));

        expect(service.dayInWeek.intervals.length).toBe(5);
    });
    
    // [{0.5, 3}, {5, 6}, {8, 9}, {11, 12.5}, {16, 18}]; -> Expected state

    it('2.2: #addInterval between {0.5, 1.5} and {2, 3} has the expected values at the expected index', () => {
        expect(service.dayInWeek.intervals[0].startTime).toBe(0.5);
        expect(service.dayInWeek.intervals[0].endTime).toBe(3);
    });

    //-----------------------------------------------------------------------------------------------------------------

    // [{0.5, 3}, {5, 6}, {8, 9}, {11, 12.5}, {16, 18}]; -> Initial state
    
    it('3.1:  #addInterval between {11, 12.5} and {16, 18} increases the length of the array to 6', () => {
        service.addInterval(new LimitInterval({
            startTime: 13,
            endTime: 14
        }));

        expect(service.dayInWeek.intervals.length).toBe(6);
    });

    // [{0.5, 3}, {5, 6}, {8, 9}, {11, 12.5}, {13, 14}, {16, 18}]; -> Expected state

    it('3.2:  #addInterval between {11, 12.5} and {16, 18} has the expected values at the expected index', () => {
        expect(service.dayInWeek.intervals[service.dayInWeek.intervals.length - 2].startTime).toBe(13);
        expect(service.dayInWeek.intervals[service.dayInWeek.intervals.length - 2].endTime).toBe(14);
    });

    //-----------------------------------------------------------------------------------------------------------------

    // [{0.5, 3}, {5, 6}, {8, 9}, {11, 12.5}, {13, 14}, {16, 18}]; -> Initial state

    it('4.1:  #addInterval after {16, 18} increases the length of the array to 7', () => {
        service.addInterval(new LimitInterval({
            startTime: 19,
            endTime: 20
        }));

        expect(service.dayInWeek.intervals.length).toBe(7);
    });

    // [{0.5, 3}, {5, 6}, {8, 9}, {11, 12.5}, {13, 14}, {16, 18}, {19, 20}]; -> Expected state

    it('4.2:  #addInterval after {16, 18} has the expected values at the expected index', () => {
        expect(service.dayInWeek.intervals[service.dayInWeek.intervals.length - 1].startTime).toBe(19);
        expect(service.dayInWeek.intervals[service.dayInWeek.intervals.length - 1].endTime).toBe(20);
    });

    //-----------------------------------------------------------------------------------------------------------------

    // [{0.5, 3}, {5, 6}, {8, 9}, {11, 12.5}, {13, 14}, {16, 18}, {19, 20}]; -> Initial state

    it('5.1:  #addInterval at {14, 15} keeps the length of the array at 7', () => {
        service.addInterval(new LimitInterval({
            startTime: 14,
            endTime: 15
        }));

        expect(service.dayInWeek.intervals.length).toBe(7);
    });

    // [{0.5, 3}, {5, 6}, {8, 9}, {11, 12.5}, {13, 15}, {16, 18}, {19, 20}]; -> Expected state

    it('5.2:  #addInterval at {14, 15} has the expected values at the expected index', () => {
        expect(service.dayInWeek.intervals[4].startTime).toBe(13);
        expect(service.dayInWeek.intervals[4].endTime).toBe(15);

        expect(service.dayInWeek.intervals[5].startTime).toBe(16);
        expect(service.dayInWeek.intervals[5].endTime).toBe(18);
    });

    //-----------------------------------------------------------------------------------------------------------------

    // [{0.5, 3}, {5, 6}, {8, 9}, {11, 12.5}, {13, 15}, {16, 18}, {19, 20}]; -> Initial state

    it('6.1:  #addInterval at {14, 19} brings the length of the array down to 5', () => {
        service.addInterval(new LimitInterval({
            startTime: 14,
            endTime: 19
        }));

        expect(service.dayInWeek.intervals.length).toBe(5);
    });

    // [{0.5, 3}, {5, 6}, {8, 9}, {11, 12.5}, {13, 20}]; -> Expected state

    it('6.2:  #addInterval at {14, 19} has the expected values at the expected index', () => {
        expect(service.dayInWeek.intervals[service.dayInWeek.intervals.length - 1].startTime).toBe(13);
        expect(service.dayInWeek.intervals[service.dayInWeek.intervals.length - 1].endTime).toBe(20);
    });

    //-----------------------------------------------------------------------------------------------------------------

    // [{0.5, 3}, {5, 6}, {8, 9}, {11, 12.5}, {13, 20}]; -> Initial state

    it('7.1:  #addInterval at {8.5, 14} brings the length of the array down to 3', () => {
        service.addInterval(new LimitInterval({
            startTime: 8.5,
            endTime: 14
        }));

        expect(service.dayInWeek.intervals.length).toBe(3);
    });

    // [{0.5, 3}, {5, 6}, {8, 20}]; -> Expected state

    it('7.2:  #addInterval at {8.5, 14} has the expected values at the expected index', () => {
        expect(service.dayInWeek.intervals[service.dayInWeek.intervals.length - 1].startTime).toBe(8);
        expect(service.dayInWeek.intervals[service.dayInWeek.intervals.length - 1].endTime).toBe(20);
    });

    //-----------------------------------------------------------------------------------------------------------------

    // [{0.5, 3}, {5, 6}, {8, 20}]; -> Initial state

    it('8.1:  #addInterval at {4.5, 6.5} keeps the length of the at 3', () => {
        service.addInterval(new LimitInterval({
            startTime: 4.5,
            endTime: 6.5
        }));

        expect(service.dayInWeek.intervals.length).toBe(3);
    });

    // [{0.5, 3}, {4.5, 6.5}, {8, 20}]; -> Expected state

    it('8.2:  #addInterval at {4.5, 6.5} has the expected values at the expected index', () => {
        expect(service.dayInWeek.intervals[service.dayInWeek.intervals.length - 2].startTime).toBe(4.5);
        expect(service.dayInWeek.intervals[service.dayInWeek.intervals.length - 2].endTime).toBe(6.5);
    });

    //-----------------------------------------------------------------------------------------------------------------

    // [{0.5, 3}, {4.5, 6.5}, {8, 20}]; -> Initial state

    it('9.1:  #addInterval at {1, 5} brings the length of the array down to 2', () => {
        service.addInterval(new LimitInterval({
            startTime: 1,
            endTime: 5
        }));
    
        expect(service.dayInWeek.intervals.length).toBe(2);
    });

    // [{0.5, 6.5}, {8, 20}]; -> Expected state

    it('9.2:  #addInterval at {1, 5} has the expected values at the expected index', () => {
        expect(service.dayInWeek.intervals[0].startTime).toBe(0.5);
        expect(service.dayInWeek.intervals[0].endTime).toBe(6.5);
    });

    //-----------------------------------------------------------------------------------------------------------------
});