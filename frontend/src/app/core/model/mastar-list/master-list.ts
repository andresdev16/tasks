import { MasterCrudService } from './../../services/master-crud/master-crud.service';
import { ICrudder } from './../../interfaces/icrudder';
import {
  AfterViewInit,
  ChangeDetectorRef,
  Directive,
  Inject,
  Injector,
  OnDestroy,
  ViewChild,
} from '@angular/core';
import { Observable, Subscription, filter, map } from 'rxjs';
import { OverlayContainerService } from '../../services/overlay-container/overlay-container.service';

import { ComponentPortal } from '@angular/cdk/portal';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { TaskFormComponent } from '../../../modules/pages/task/containers/task-form/task-form.component';

export interface IMasterListConfig {
  uri: string;
  uriComplement: string;
}

@Directive()
export abstract class MasterList implements OnDestroy, AfterViewInit {
  crudder: ICrudder;
  dataSource: MatTableDataSource<any> = new MatTableDataSource<any>([]);
  length = 50;
  pageSize = 5;
  pageIndex = 0;
  private _offset = 0;

  @ViewChild(MatPaginator) paginator?: MatPaginator;

  protected subscriptions: Subscription[] = [];

  constructor(
    protected crudService: MasterCrudService,
    @Inject('masterConfig') protected masterConfig: IMasterListConfig,
    protected overlayContainerService: OverlayContainerService,
    protected changeDetectorRef: ChangeDetectorRef
  ) {
    this.crudder = crudService.getCrudder(
      masterConfig.uri,
      masterConfig.uriComplement
    );
    this.search();
  }

  ngAfterViewInit() {
    this.changeDetectorRef.detectChanges();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
      console.log(this.dataSource);
    }
  }

  edit(id: number): void {
    this._onForm(id);
  }

  createItem(): void {
    this._onForm();
  }

  search() {
    this._getItems();
  }

  remove(id: string) {
    this._deleteItem(id);
  }

  handlePageEvent(event: PageEvent) {
    debugger;
    this.length = event.length;
    this.pageSize = event.pageSize;
    this._offset =
      event.pageIndex > this.pageIndex
        ? this._offset + this.pageSize
        : this.pageSize - this._offset;
    this.pageIndex = event.pageIndex;
    this._getItems();
  }

  completed(id: string): void {
    const sub = this.crudService
      .put(`${this.masterConfig.uri}/finish`, { taskId: id })
      .subscribe((_) => {
        this._getItems();
      });
    this.subscriptions.push(sub);
  }

  private _getItems(): void {
    const sub = this.crudder
      .get(this._offset, this.pageSize)
      .subscribe(({ tasks, total }) => {
        this.length = total;
        this._processDataFilter(tasks);
      });
    this.subscriptions.push(sub);
  }

  private _processDataFilter(content: any[]): void {
    this.dataSource = new MatTableDataSource(content);
  }

  private _deleteItem(itemId: string) {
    this.crudder.delete(itemId).subscribe(() => {
      this.dataSource.data = [];
      this.search();
    });
  }

  private _onForm(id?: number): void {
    const data = id
      ? this.dataSource.data.find((value) => value?.id === id)
      : {};
    const componentPortal = new ComponentPortal(
      TaskFormComponent,
      null,
      Injector.create({
        providers: [
          {
            provide: 'dataForm',
            useValue: {
              data,
            },
          },
        ],
      })
    );
    const componentPortalInstance =
      this.overlayContainerService.open(componentPortal);
    const sub = componentPortalInstance?.exit?.subscribe(() => {
      this.dataSource.data = [];
      this.search();
    });
    this.subscriptions.push(sub);
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub && sub.unsubscribe());
    if (this.dataSource) {
      this.dataSource.disconnect();
    }
  }
}
