type Props = {
    pageSize: number;
    currentPage: number;
    totalPages: number;
}

function PageStats(props: Props) {
    return (
        <section className="flex flex-col items-center pb-2 px-2">
            <div>
                <h3 className="stat-title font-bold">Page Stats</h3>
            </div>
            <div>
                <div className="stats shadow">
                    <section className="stat place-items-center px-4 sm:px-6">
                        <h4 className="stat-desc">Page Size</h4>
                        <p className="stat-value">{props.pageSize}</p>
                    </section>
                    <section className="stat place-items-center px-4 sm:px-6">
                        <h4 className="stat-desc">Current Page</h4>
                        <p className="stat-value text-secondary">{props.currentPage}</p>
                    </section>
                    <section className="stat place-items-center px-4 sm:px-6">
                        <h4 className="stat-desc">Total Pages</h4>
                        <p className="stat-value">{props.totalPages}</p>
                    </section>
                </div>
            </div>
        </section>
    );
}

export default PageStats;
