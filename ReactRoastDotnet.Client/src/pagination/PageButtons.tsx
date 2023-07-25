const paginationButtonClass = "join-item btn btn-outline ";
const prevButtonClass = paginationButtonClass + "btn-secondary";
const nextButtonClass = paginationButtonClass + "btn-primary";

type Props = {
    onPrevious: VoidFunction;
    onNext: VoidFunction;
    hasPrevious: boolean;
    hasNext: boolean;
}

function PageButtons(props: Props) {
    return (
        <menu className="join grid grid-cols-2 ml-auto mr-auto max-w-screen-md px-1 sm:px-0">
            <button className={prevButtonClass} onClick={props.onPrevious} disabled={!props.hasPrevious}>
                Previous page
            </button>
            <button className={nextButtonClass} onClick={props.onNext} disabled={!props.hasNext}>
                Next Page
            </button>
        </menu>
    );
}

export default PageButtons;
